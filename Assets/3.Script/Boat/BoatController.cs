using UnityEngine;
using UnityEngine.InputSystem;

public class BoatController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float speedUp = 1f;
    private Rigidbody rb;
    private float curSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Boat!");

        float input = context.ReadValue<Vector2>().x;

        if (input != 0)
        {
            float speed = maxSpeed * -input;
            curSpeed = Mathf.MoveTowards(curSpeed, speed, speedUp * Time.deltaTime);
            rb.velocity = new Vector3(curSpeed, rb.velocity.y, 0);
            Flip(input);
        }
        else
        {
            curSpeed = Mathf.MoveTowards(curSpeed, 0, speedUp * Time.deltaTime);
            rb.velocity = new Vector3(curSpeed, rb.velocity.y, 0);
        }
    }

    private void Flip(float dir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(dir);
        transform.localScale = scale;
    }
}