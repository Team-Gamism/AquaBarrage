using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private bool isExplosion = false;

    private void Start()
    {
        StartCoroutine(Disappoint());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BoatController>() && !isExplosion)
        {
            isExplosion = true;
            GameManager.Instance.effectAudioSource.PlayOneShot(audioClip);
            GameManager.Instance.CurHP--;
        }
    }

    IEnumerator Disappoint()
    {
        yield return new WaitForSeconds(0.7f);
        GetComponent<SphereCollider>().enabled = false;
    }
}