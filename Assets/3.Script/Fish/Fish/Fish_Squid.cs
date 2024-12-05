using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Squid : Fish, ICanFish
{
    [SerializeField] private float riseSpeed = 15f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float detectionRange = 50f;
    [SerializeField] private float targetHeight = 1.25f;
    private bool isPlayerDetect = false;
    private bool isRising = false;
    private bool isExploded = false;
    private bool isCaught = false;

    protected override void Awake()
    {
        Init();
    }

    protected override void FixedUpdate()
    {
        if (isCaught || isExploded) return;

        if (isRising)
        {
            RiseUp();
        }
        else
        {
            Move();
            Rotate();
            CheckPlayerDetection();
        }
    }

    private void CheckPlayerDetection()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, detectionRange))
        {
            if (hit.collider != null && hit.collider.CompareTag("Player") && !isPlayerDetect)
            {
                isPlayerDetect = true;
                DetectPlayerExplosion();
            }
        }
    }

    private void DetectPlayerExplosion()
    {
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        isRising = true;
    }

    private void RiseUp()
    {
        if (transform.position.y < targetHeight)
        {
            transform.Translate(Vector3.up * riseSpeed * Time.deltaTime, Space.World);
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (!isExploded)
        {
            isExploded = true;

            GameObject explosionPrefab = Resources.Load<GameObject>("Fish/Effect/Explosion");
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.up * detectionRange);
    }

    public override Transform Fished(Transform hook)
    {
        isCaught = true;
        StopAllCoroutines();
        return base.Fished(hook);
    }
}