using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fin : MonoBehaviour
{
    public float speed;

    private Rigidbody rigid;

    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject parryingEffect;
    [SerializeField] AudioClip parryingAudio;
    private void Awake()
    {
        transform.parent = null;

        rigid = GetComponent<Rigidbody>();
        Destroy(gameObject, 15f);

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null && !GameManager.Instance.isDash)
            {
                if(SceneManager.GetActiveScene().buildIndex != 5)
                 GameManager.Instance.CurHP--;
                Instantiate(hitEffect, other.transform.position, Quaternion.identity);
            }
            else
            {
                if (SceneManager.GetActiveScene().buildIndex == 5)
                    TutorialController.parryingAcion?.Invoke();
                Instantiate(parryingEffect, other.transform.position + new Vector3(0, 1, 0), Quaternion.identity, other.transform);
                GameManager.Instance.effectAudioSource.PlayOneShot(parryingAudio);
            }

            Destroy(gameObject);
        }
    }
}