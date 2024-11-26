using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanFish
{
    public void Fished(Collider fish, Vector3 lastPostion);
}

public class Fish : MonoBehaviour, ICanFish
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
        Left,
        Right
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
        transform.Translate((fish_Direction == Fish_Direction.Left ? 1 : -1) * speed * transform.right / 2 * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    private void Rotate()
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

    private IEnumerator AttackCoroutine()
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

    private void Init()
    {
        fishName = fishStat.fishName;
        speed = fishStat.speed;
        weight = fishStat.weight;
        attackCoolTime = fishStat.attackCoolTime;

        if(fish_Direction == Fish_Direction.Right)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -transform.rotation.eulerAngles.z));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Deleter"))
        {
            Destroy(gameObject);
        }
    }

   public void Fished(Collider other, Vector3 lastPosition)
    {
        Transform fish = other.transform;

        MonoBehaviour fishScript = fish.GetComponent<Fish>();
        if (fishScript != null)
        {
            fishScript.enabled = false;
        }

        fish.SetParent(transform);

        fish.localPosition = Vector3.zero;

        Vector3 movementDirection = (transform.position - lastPosition).normalized;

        if (movementDirection.sqrMagnitude > 0.001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            fish.rotation = lookRotation;
        }
    }
}
