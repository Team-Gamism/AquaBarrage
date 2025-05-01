using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionEffect;
    private MeshRenderer meshRenderer;
    private Collider cannonBallCollider;
    private Rigidbody rb;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        cannonBallCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (meshRenderer != null) meshRenderer.enabled = false;
            if (cannonBallCollider != null) cannonBallCollider.enabled = false;

            if (rb != null) rb.isKinematic = true;

            if (explosionEffect != null)
            {
                explosionEffect.transform.position = transform.position;
                explosionEffect.Play();
            }
            GameManager.Instance.CurHP--;

            StartCoroutine(DestroyAfterExplosion());
        }
    }

    private IEnumerator DestroyAfterExplosion()
    {
        yield return new WaitForSeconds(explosionEffect.main.duration);
        Destroy(gameObject);
    }
}