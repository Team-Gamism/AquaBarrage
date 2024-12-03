using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pause : MonoBehaviour
{
    public GameObject checkLeaveUI;

    public GameObject settingUI;
    [SerializeField] AudioClip clickAudio;
    private void OnEnable()
    {
        Time.timeScale = 0;
        GameManager.UI.AddPopupUI(gameObject);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        GameManager.UI.RemovePopupUI(gameObject);
    }

    public void Continue()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        Destroy(gameObject);
    }

    public void Setting()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        Instantiate(settingUI);
    }

    public void Home()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        Instantiate(checkLeaveUI);
    }
}
