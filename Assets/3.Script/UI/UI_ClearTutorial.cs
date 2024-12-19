using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ClearTutorial : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
    }

    public void Play()
    {
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        GameManager.Instance.InitData();
        UI_Fade.instance.FadeIn();
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(1);
    }

    public void Home()
    {
       StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(1);
    }
}
