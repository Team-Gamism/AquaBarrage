using UnityEngine;
using UnityEngine.InputSystem;

public class BoatController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float speedUp = 1f;
    [SerializeField] private float speedDown = 2f;
    private Rigidbody rb;
    private float curSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        if (input.x != 0)
        {
            float speed = maxSpeed * input.x;
            curSpeed = Mathf.MoveTowards(curSpeed, speed, speedUp * Time.deltaTime);
        }
        else
        {
            curSpeed = 0f;
        }
        rb.velocity = new Vector3(curSpeed, rb.velocity.y, 0);

        if (input.x != 0)
        {
            Flip(input.x);
        }
    }

    private void Flip(float dir)
    {
        Vector3 scale = transform.localScale;
        scale.x = dir > 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}