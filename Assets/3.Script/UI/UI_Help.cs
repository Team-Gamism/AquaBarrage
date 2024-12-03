using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Help : MonoBehaviour
{
    [SerializeField] AudioClip clickAudio;
    public void Leave()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainScene");
    }
}
