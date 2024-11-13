using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public FishStatSO fishStat;

    public Transform[] bulletPoints;

    private string fishName;
    private float speed;
    private float weight;
    private float attackCoolTime;

    private float turnVecX = 0f;

    public LayerMask groundLayer;
    public LayerMask seaBorderLayer;

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
        StartCoroutine(AttackCoroutine());
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();

        CheckGround();
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
            turnVecX, Time.deltaTime*Time.deltaTime) - (fish_Direction == Fish_Direction.left ? 0 : 180), -90f, fish_Direction == Fish_Direction.left ? 0 : -180));

    }

    IEnumerator TurnCoroutine()
    {
        while (true)
        {
            turnVecX = Random.Range(-30f, 40f);

            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }

    IEnumerator AttackCoroutine()
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

    void CheckGround()
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

    void Init()
    {
        fishName = fishStat.fishName;
        speed = fishStat.speed;
        weight = fishStat.weight;
        attackCoolTime = fishStat.attackCoolTime;

        if(fish_Direction == Fish_Direction.right)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -transform.rotation.eulerAngles.z));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Deleter")
        {
            Destroy(gameObject);
        }
    }
}
