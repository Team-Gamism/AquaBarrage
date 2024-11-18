using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public FishStatSO fishInfo;

    public Transform[] bulletPoints;

    private string fishName;
    private float speed;
    private float weight;
    private float attackCoolTime;

    private float turnVecX = 0f;

    public LayerMask groundLayer;
    public LayerMask seaBorderLayer;
    public LayerMask wallLayer;

    public GameObject pattern2Atk;
    public GameObject pattern3Atk;

    public enum Fish_Direction
    {
        left,
        right
    }

    public Fish_Direction fish_Direction;

    void Awake()
    {
        Init();
        StartCoroutine(TurnCoroutine());
        StartCoroutine(Pattern1());
    }

    IEnumerator Pattern1()
    {
        Debug.Log("패턴1");
        StartCoroutine(Pattern3());
        while (isSkill3)
        {
            yield return null;
            Move();
            Rotate();

            CheckGround();
        }
        switch (Random.Range(0, 2))
        {
            case 0:
                StartCoroutine(Pattern1());
                break;
            case 1:
                StartCoroutine(Pattern2());
                break;
        }
        
    }
    IEnumerator Pattern2()
    {
        Debug.Log("패턴2");
        yield return null;
        transform.DOMove(new Vector3(-50,-19,0),6).onComplete += () => 
        {
            transform.DOLookAt(new Vector3(45, -19, 0), 1);
            StartCoroutine(Pattern2Attack());
            transform.DOMove(new Vector3(50, -19, 0), 12).onComplete += () => 
            {
                fish_Direction = Fish_Direction.left;
                transform.DOLookAt(new Vector3(0, -15.5f, 0), 1);
                transform.DOMove(new Vector3(0, -15.5f, 0), 4).onComplete += () => { StartCoroutine(Pattern1()); };
            };
        };
        transform.DOLookAt(new Vector3(-50, -19, 0), 1);
    }

    IEnumerator Pattern2Attack()
    {
        yield return new WaitForSeconds(1.2f);
        for (int i = 0;i< 5; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.5f,1.2f));
            Instantiate(pattern2Atk, transform);
        }
    }

    bool isSkill3;

    IEnumerator Pattern3()
    {
        isSkill3 = true;
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(Random.Range(2,3.5f));
            Instantiate(pattern3Atk, transform);
        }
        yield return new WaitForSeconds(3);
        isSkill3 = false;
    }

    private void Move()
    {
        transform.Translate((fish_Direction == Fish_Direction.left ? 1 : -1) * speed * transform.right / 2 * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(
            Mathf.Lerp(transform.rotation.eulerAngles.x >= 180 ?
            transform.rotation.eulerAngles.x - 360 : transform.rotation.eulerAngles.x,
            turnVecX, Time.deltaTime * 1.5f) + (fish_Direction == Fish_Direction.left ? 0 : 180), -90, 0));

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0f);

    }

    IEnumerator TurnCoroutine()
    {
        while (true)
        {
            turnVecX = Random.Range(-30f, 40f);

            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }

    void CheckGround()
    {
        if (Physics.Raycast(transform.position, transform.forward, 5f, groundLayer))
        {
            turnVecX = Random.Range(-30f, -16);
        }

        if (Physics.Raycast(transform.position, transform.forward, 5f, seaBorderLayer))
        {
            turnVecX = Random.Range(30f, 16);
        }

        if (Physics.Raycast(transform.position, transform.forward, 5f, wallLayer))
        {
            if (fish_Direction == Fish_Direction.right)
            {
                fish_Direction = Fish_Direction.left;
                transform.DORotate(new Vector3(0, -90, 0), 1);
            }
            else
            {
                fish_Direction = Fish_Direction.right;
                transform.DORotate(new Vector3(0, 90, 0), 1);
            }
        }
    }

    void Init()
    {
        fishName = fishInfo.fishName;
        speed = fishInfo.speed;
        weight = fishInfo.weight;
        attackCoolTime = fishInfo.attackCoolTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, (fish_Direction == Fish_Direction.left ? 1 : -1) * transform.forward * 2f);
    }
}
