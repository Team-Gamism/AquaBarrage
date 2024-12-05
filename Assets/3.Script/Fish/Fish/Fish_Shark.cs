using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Shark : MonoBehaviour, ICanFish
{
    public FishStatSO fishInfo;

    public int maxHp = 100;

    int hp;

    public UI_BossHpBar hpBar;
    
    public Transform[] bulletPoints;

    private string fishName;
    private float speed;
    private float weight;
    private float attackCoolTime;

    private float turnVecX = 0f;

    public LayerMask groundLayer;
    public LayerMask seaBorderLayer;
    public LayerMask wallLayer;

    public GameObject pattern1Atk;
    public GameObject pattern2Atk;
    public GameObject pattern3Atk;

    bool isSkill;

    public enum Fish_Direction
    {
        Left,
        Right
    }

    public Fish_Direction fish_Direction;

    void Awake()
    {
        Init();
        StartCoroutine(TurnCoroutine());
        StartCoroutine(ChoicePattern());
        StartCoroutine(Move());
        StartCoroutine(Rotate());
        hp = maxHp;
        hpBar.SetHpBar(hp / maxHp);
    }

    IEnumerator ChoicePattern()
    {
         yield return null;
        switch (Random.Range(0, 3))
        {
            case 0:
                StartCoroutine(Pattern1());
                break;
            case 1:
                StartCoroutine(Pattern2());
                break;
            case 2:
                StartCoroutine(Pattern3());
                break;
        }
        
    }
    IEnumerator Pattern1()
    {
        yield return null;
        isSkill = true;
        transform.DOMove(new Vector3(-50,-19,0),6).onComplete += () => 
        {
            transform.DOLookAt(new Vector3(45, -19, 0), 1);
            StartCoroutine(Pattern1Attack());
            transform.DOMove(new Vector3(50, -19, 0), 12).onComplete += () => 
            {
                fish_Direction = Fish_Direction.Left;
                transform.DOLookAt(new Vector3(0, -15.5f, 0), 1);
                transform.DOMove(new Vector3(0, -15.5f, 0), 4).onComplete += () => { isSkill = false; StartCoroutine(ChoicePattern()); };
            };
        };
        transform.DOLookAt(new Vector3(-50, -19, 0), 1);
        
    }

    IEnumerator Pattern1Attack()
    {
        yield return new WaitForSeconds(1.2f);
        for (int i = 0;i< 5; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.5f,1.2f));
            Instantiate(pattern1Atk, transform);
        }
    }

    

    IEnumerator Pattern2()
    { 
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(Random.Range(2,3.5f));
            Instantiate(pattern2Atk, transform);
        }
        yield return new WaitForSeconds(3);
        StartCoroutine(ChoicePattern());
    }

    IEnumerator Pattern3()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(Random.Range(3f, 4.5f));
            Instantiate(pattern3Atk, transform);
        }
        yield return new WaitForSeconds(3);
        StartCoroutine(ChoicePattern());
    }

    IEnumerator Move()
    {
        while (true)
        {
            yield return null;
            if (isSkill)
                continue;

            transform.Translate((fish_Direction == Fish_Direction.Left ? 1 : -1) * speed * transform.right / 2 * Time.deltaTime);

            CheckGround();
        }
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            yield return null;
            if (isSkill)
                continue;
            transform.rotation = Quaternion.Euler(new Vector3(
           Mathf.Lerp(transform.rotation.eulerAngles.x >= 180 ?
           transform.rotation.eulerAngles.x - 360 : transform.rotation.eulerAngles.x,
           turnVecX, Time.deltaTime * Time.deltaTime) - (fish_Direction == Fish_Direction.Left ? 0 : 180), -90f, fish_Direction == Fish_Direction.Left ? 0 : -180));
        }
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

        if (Physics.Raycast(transform.position, transform.forward, 10f, wallLayer))
        {
            if (fish_Direction == Fish_Direction.Right)
            {
                fish_Direction = Fish_Direction.Left;
                transform.DORotate(new Vector3(0, -90, 0), 1);
            }
            else
            {
                fish_Direction = Fish_Direction.Right;
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

        Gizmos.DrawRay(transform.position, (fish_Direction == Fish_Direction.Left ? 1 : -1) * transform.forward * 10f);
    }

    public Transform Fished(Transform hook)
    {
        hp -= 10;
        hpBar.SetHpBar((float)hp / maxHp);

        if (hp <= 0)
        {
            gameObject.SetActive(false);
            gameObject.SetActive(true);

            transform.SetParent(hook);

            transform.localPosition = Vector3.zero;

            GameManager.Instance.Money += fishInfo.money;

            return transform;
        }
        else
            return null;
    }

    private void OnDestroy()
    {
        LevelManager.instance.isBossCut = true;
    }
}
