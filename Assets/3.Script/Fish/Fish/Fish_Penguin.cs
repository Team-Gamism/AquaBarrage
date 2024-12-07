using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Penguin : Fish, IFishable
{
    private Animator anim;
    private bool isAttack = false;
    private bool isCollidable = false;

    protected override void Awake()
    {
        anim = GetComponent<Animator>();

        Init();
        StartCoroutine(AttackCoroutine());
    }

    protected override void FixedUpdate()
    {
        if (!isAttack)
        {
            anim.Play("CustomPenguinSwim");

            Rotate();
        }
        if (transform.position.y < -10f)
        {
            isCollidable = false;
        }
        Move();
    }

    protected override void Move()
    {
        transform.parent.position += new Vector3((fish_Direction == Fish_Direction.Left ? -1 : 1) * speed * Time.deltaTime, 0, 0);
    }

    protected override void Attack()
    {
        if (!isAttack)
        {
            JumpAttack();
        }
    }

    private void JumpAttack()
    {
        isAttack = true;

        if (fish_Direction == Fish_Direction.Left)
        {
            anim.SetTrigger("LeftAttack");
        }
        else
        {
            anim.SetTrigger("RightAttack");
        }
    }

    protected override IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if (!isAttack)
            {
                yield return new WaitForSeconds(attackCoolTime + Random.Range(-1, 2));
                Attack();
            }
            yield return null;
        }
    }

    public void OnAttackAnimationEnd()
    {
        isAttack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollidable)
        {
            isCollidable = true;
            GameManager.Instance.CurHP--;
        }
    }
}