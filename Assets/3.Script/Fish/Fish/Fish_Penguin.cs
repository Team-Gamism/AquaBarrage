using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Penguin : Fish, ICanFish
{
    [SerializeField] private float targetHeight = 1.25f;
    [SerializeField] private float jumpPower = 2f;
    private float initialYPos;
    private bool isAttack = false;

    protected override void Awake()
    {
        Init();
        initialYPos = transform.position.y;
        StartCoroutine(AttackCoroutine());
    }

    protected override void FixedUpdate()
    {
        if (!isAttack)
        {
            Move();
            Rotate();
        }
    }

    protected override void Attack()
    {
        if (!isAttack)
        {
            StartCoroutine(JumpAttack());
        }
    }

    private IEnumerator JumpAttack()
    {
        isAttack = true;
        int dir = fish_Direction == Fish_Direction.Left ? -1 : 1;

        while (transform.position.y < targetHeight)
        {
            float x = dir * Time.deltaTime;
            Vector3 Dir = new Vector3(x, Mathf.Sin(jumpPower * Time.deltaTime), 0f);
            transform.position += Dir;
            Rotate(Dir);
            yield return null;
        }
        Vector3 direction = new Vector3(dir * (jumpPower / 3), 0f, 0f);
        transform.position = Vector3.Lerp(transform.position, direction, Time.deltaTime);
        Rotate(direction);

        while (transform.position.y > initialYPos)
        {
            float x = dir * Time.deltaTime;
            Vector3 Dir = new Vector3(x, Mathf.Sin(-jumpPower * Time.deltaTime), 0f);
            transform.position += Dir;
            Rotate(Dir);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0f, dir * 90, 0f);
        isAttack = false;
    }

    private void Rotate(Vector3 dir)
    {
        if (dir == Vector3.zero) return;

        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }
}