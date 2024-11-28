using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float speedUp = 2f;
    [SerializeField] private float speedDown = 3f;
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    private Rigidbody rb;
    private float curSpeed = 0f;
    private float targetSpeed = 0f;

    private float minXPos = -23.5f;
    private float maxXPos = 23.5f;

    bool canDash = true;

    private float dashCool = 2;

    public PlayerStatSO engineSO;
    public PlayerStatSO dashSO;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isDash)
        {
            curSpeed = LevelManager.instance.isEndGame
                ? 0f
                : Mathf.MoveTowards(curSpeed, targetSpeed,
                    (targetSpeed == 0 ? speedDown : speedUp) * Time.fixedDeltaTime);
            rb.velocity = new Vector3(curSpeed + engineSO.valueList[GameManager.Instance.engineLevel], rb.velocity.y, 0);
        }
        float x = Mathf.Clamp(transform.position.x, minXPos, maxXPos);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public void OnMove(InputValue value)
    {
        //if (GameManager.Instance.isDash) return;

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

    public void OnDash(InputValue value)
    {
        if (canDash && value.isPressed)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        GameManager.Instance.isDash = true;
        float dir = targetSpeed > 0 ? 1 : -1;
        rb.velocity = new Vector3(dashForce * dir, rb.velocity.y, 0);
        yield return new WaitForSeconds(dashDuration);
        GameManager.Instance.isDash = false;
        yield return new WaitForSeconds(dashCool - dashSO.valueList[GameManager.Instance.dashLevel]);
        canDash = true;
    }

    private void Flip(float dir)
    {
        if (dir == 0) return;
        Vector3 scale = transform.localScale;
        scale.x = dir > 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}