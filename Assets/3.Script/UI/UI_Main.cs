using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Main : MonoBehaviour
{
    public GameObject settingUI;
    //public GameObject checkPlayUI;
    [SerializeField] AudioClip clickAudio;
    private void Start()
    {
        /* 수정전
        GameManager.Instance.InitData();
        */
        GameObject go = Instantiate(settingUI);
        go.GetComponent<UI_Setting>().FirstSet();
        Destroy(go);

        StartCoroutine(CheckAnyKey());
    }

    /* 수정전
    public void ClickPlay()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        Instantiate(checkPlayUI);
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
        SceneManager.LoadScene("HelpScene2");
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
    */

    IEnumerator CheckAnyKey()
    {
        while (true)
        {
            yield return null;
            if (!Input.anyKeyDown)
                continue;

            Play();
            yield break;
        }
    }
    public void Play()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSeconds(2f);
        GameManager.Instance.isPlayGame = true;
        SceneManager.LoadScene("GameScene_UI_HC");
    }
}
