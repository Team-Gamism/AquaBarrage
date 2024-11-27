using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float speedUp = 2f;
    [SerializeField] private float speedDown = 3f;
    private Rigidbody rb;
    private float curSpeed = 0f;
    private float targetSpeed = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        curSpeed = Mathf.MoveTowards(curSpeed, targetSpeed, (targetSpeed == 0 ? speedDown : speedUp) * Time.deltaTime);
        rb.velocity = new Vector3(curSpeed, rb.velocity.y, 0);
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        float dir = input.x;

        if (dir != 0)
        {
            targetSpeed = maxSpeed * dir;
            Flip(dir);
        }
        else
        {
            targetSpeed = 0;
        }
    }

    private void Flip(float dir)
    {
        if (dir == 0) return;
        Vector3 scale = transform.localScale;
        scale.x = dir > 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}