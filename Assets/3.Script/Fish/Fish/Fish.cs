using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanFish
{
    public Transform Fished(Transform hook);

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

    protected virtual void Awake()
    {
        Init();
        StartCoroutine(TurnCoroutine());
        StartCoroutine(AttackCoroutine());
    }

    protected virtual void FixedUpdate()
    {
        Move();
        Rotate();

        CheckGround();
    }

    protected void Move()
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

    protected IEnumerator AttackCoroutine()
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

    protected void Init()
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

   public virtual Transform Fished(Transform hook)
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        transform.SetParent(hook);

        transform.localPosition = Vector3.zero;

        GameManager.Instance.Money += fishStat.money;
        GameManager.Instance.fishCount++;

        return transform;
    }
}
