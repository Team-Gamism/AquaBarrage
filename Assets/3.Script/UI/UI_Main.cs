using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Main : MonoBehaviour
{
    public GameObject settingUI;
    private void Start()
    {
        GameManager.Instance.InitData();
        
    }

    public void ClickPlay()
    {
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
    }

    public void ClickHelp()
    {
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
        Application.Quit();
    }
}
