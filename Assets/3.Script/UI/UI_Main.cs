using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    public Text nameText;

    private void Start()
    {
        GameManager.Instance.InitData();
    }

    public void ClickPlay()
    {
        if(nameText.text != "")
        {
            GameManager.Instance.InitData();
        }
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
