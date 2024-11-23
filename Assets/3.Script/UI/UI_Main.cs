using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Main : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.InitData();
    }

    public void ClickPlay()
    {
            GameManager.Instance.InitData();

            SceneManager.LoadScene("GameScene");
    }

    public void ClickHelp()
    {
        SceneManager.LoadScene("HelpScene");
    }

    public void ClickRank()
    {
        SceneManager.LoadScene("RankScene");
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
