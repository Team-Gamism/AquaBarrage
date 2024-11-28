using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.effectAudioSource = GetComponent<AudioSource>();
    }
}
