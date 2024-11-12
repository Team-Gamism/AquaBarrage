using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fin : MonoBehaviour
{
    public float speed;

    private Rigidbody rigid;

    private void Awake()
    {
        transform.parent = null;

        rigid = GetComponent<Rigidbody>();
        Destroy(gameObject, 6f);

        StartCoroutine(Effect());
    }

    private void FixedUpdate()
    {
        rigid.velocity = transform.up * speed;
        rigid.angularVelocity = Vector3.zero;
    }

    protected virtual IEnumerator Effect()
    {
        yield return null;
    }
}
