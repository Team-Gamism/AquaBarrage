using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public interface IFishable
{
    public Transform Fished(Transform hook);
    public int money { get; }
}

public class Fish : MonoBehaviour, IFishable
{
    public Action fishedAcion; 

    public FishStatSO fishStat;

    public Transform[] bulletPoints;

    protected string fishName;
    protected float speed;
    protected float weight;
    protected float attackCoolTime;

    protected float turnVecX = 0f;

    public LayerMask groundLayer;
    public LayerMask seaBorderLayer;

    public enum Fish_Direction
    {
        Left,
        Right
    }

    public Fish_Direction fish_Direction;

    public int money { get => fishStat.money; }

    protected virtual void Awake()
    {
        Init();
        StartCoroutine(TurnCoroutine());
        StartCoroutine(AttackCoroutine());
    }

    protected virtual void Init()
    {
        fishName = fishStat.fishName;
        speed = fishStat.speed;
        weight = fishStat.weight;
        attackCoolTime = fishStat.attackCoolTime;

        if (fish_Direction == Fish_Direction.Right)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -transform.rotation.eulerAngles.z));
        }
    }


    protected virtual void FixedUpdate()
    {
        Move();
        Rotate();

        CheckGround();
    }

    protected virtual void Move()
    {
        transform.Translate((fish_Direction == Fish_Direction.Left ? 1 : -1) * speed * transform.right / 2 * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    protected void Rotate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(
            Mathf.Lerp(transform.rotation.eulerAngles.x >= 180 ?
            transform.rotation.eulerAngles.x - 360 : transform.rotation.eulerAngles.x,
            turnVecX, Time.deltaTime*Time.deltaTime) - (fish_Direction == Fish_Direction.Left ? 0 : 180), -90f, fish_Direction == Fish_Direction.Left ? 0 : -180));

    }

    private IEnumerator TurnCoroutine()
    {
        while (true)
        {
            turnVecX = Random.Range(-30f, 40f);

            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }

    protected virtual IEnumerator AttackCoroutine() 
    {
        while (true)
        {
            yield return new WaitForSeconds(attackCoolTime + Random.Range(-1, 2));
            Attack();
        }
    }

    protected virtual void Attack()
    {

    }

    private void CheckGround()
    {
        if (Physics.Raycast(transform.position, transform.forward, 2f, groundLayer))
        {
            turnVecX = Random.Range(-30f, -16);
        }

        if (Physics.Raycast(transform.position, transform.forward, 2f, seaBorderLayer))
        {
            turnVecX = Random.Range(30f, 16);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }

    public virtual Transform Fished(Transform hook)
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        transform.SetParent(hook);
        fishedAcion?.Invoke();
        transform.localPosition = Vector3.zero;

        GameManager.Instance.fishCount++;

        return transform;
    }
}
