using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Main : MonoBehaviour
{
    public Text nameText;

    private void Start()
    {
        GameManager.Instance.InitData();
    }

    public void ClickPlay()
    {
        if (nameText.text != "")
        {
            GameManager.Instance.InitData();
            GameManager.Instance.playerName = nameText.text;

            SceneManager.LoadScene("GameScene");
        }
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
