using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.effectAudioSource.PlayOneShot(audioClip);
            GameManager.Instance.CurHP--;
        }
    }
}