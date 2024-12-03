using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_CheckLeave : MonoBehaviour
{
    [SerializeField] AudioClip clickAudio;
    private void OnEnable()
    {
        GameManager.UI.AddPopupUI(gameObject);
    }

    private void OnDisable()
    {
        GameManager.UI.RemovePopupUI(gameObject);
    }

    public void Leave()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainScene");
    }

    public void Cancel()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        Destroy(gameObject);
    }
}
