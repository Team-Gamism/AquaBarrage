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
    float seeDir;

    bool canDash = true;

    private float dashCool = 1.2f;

    public PlayerStatSO engineSO;
    public PlayerStatSO dashSO;

    AudioSource audioSource;

    [SerializeField] AudioClip dashAudio;
    [SerializeField] AudioClip hitAudio;

    [SerializeField] GameObject hitEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.hitEvent = Hited;
        GameManager.Instance.player = transform;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isDash && GameManager.Instance.CurHP > 0)
        {
            curSpeed = Mathf.MoveTowards(curSpeed, targetSpeed,
                    (targetSpeed == 0 ? speedDown : speedUp) * Time.fixedDeltaTime);
            rb.velocity = new Vector3(curSpeed, rb.velocity.y, 0);
        }
        float x = Mathf.Clamp(transform.position.x, minXPos, maxXPos);
        transform.position = new Vector3(x, -2.5f, transform.position.z);
    }

    public void OnMove(InputValue value)
    {
        if (GameManager.Instance.CurHP <= 0) return;

        Vector2 input = value.Get<Vector2>();
        float dir = input.x;

        if (dir != 0)
        {
            targetSpeed = (engineSO.valueList[GameManager.Instance.engineLevel] + maxSpeed) * dir;
            seeDir = dir;
            Flip(dir);
        }
        else
        {
            targetSpeed = 0;
        }
    }

    public void OnDash(InputValue value)
    {
        if (GameManager.Instance.CurHP <= 0) return;
        if (canDash && value.isPressed)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        audioSource.PlayOneShot(dashAudio);
        GameManager.Instance.isDash = true;
        rb.velocity = new Vector3(dashForce * seeDir, rb.velocity.y, 0);
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

    void Hited()
    {
        Instantiate(hitEffect,transform.position + new Vector3(0,1,0),Quaternion.identity);
        audioSource.PlayOneShot(hitAudio);
    }
}