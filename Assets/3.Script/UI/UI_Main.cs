using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Main : MonoBehaviour
{
    public GameObject settingUI;
    [SerializeField] AudioClip clickAudio;
    private void Start()
    {
        GameManager.Instance.InitData();
        GameObject go = Instantiate(settingUI);
        go.GetComponent<UI_Setting>().FirstSet();
        Destroy(go);
    }

    public void ClickPlay()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        GameManager.Instance.isPlayGame = true;
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameScene_UI_HC");
    }

    public void ClickSetting()
    {
        Instantiate(settingUI);
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
    }

    public void ClickHelp()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        StartCoroutine(LoadHelpScene());
    }

    IEnumerator LoadHelpScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("HelpScene");
    }

    public void ClickRank()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        StartCoroutine(LoadRankScene());
    }

    IEnumerator LoadRankScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("RankScene");
    }

    public void ClickExit()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        Application.Quit();
    }
}
