using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public FishInfoSO fishInfo;

    private string fishName;
    private float speed;
    private float weight;

    private float turnVecX = 0f;

    public LayerMask groundLayer;

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
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(
            Mathf.Lerp(transform.rotation.eulerAngles.x >= 180 ?
            transform.rotation.eulerAngles.x - 360 : transform.rotation.eulerAngles.x,
            turnVecX, Time.deltaTime * 1.5f), -90, 0));
    }

    IEnumerator TurnCoroutine()
    {
        turnVecX = Random.Range(-30f, 40f);

        yield return new WaitForSeconds(Random.Range(2f, 5f));
        StartCoroutine(TurnCoroutine());
    }

    void CheckGround()
    {
        if (Physics.Raycast(transform.position, transform.forward, 1.5f, groundLayer))
        {
            turnVecX = Random.Range(-30f, -16);
        }
    }

    void Init()
    {
        fishName = fishInfo.fishName;
        speed = fishInfo.speed;
        weight = fishInfo.weight;
    }
}
