using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Game : MonoBehaviour
{
    public InputField nameInput;
    public Text stageText;
    public Text timerText;
    public GameObject[] hearts;
    public GameObject[] blackHearts;

    private float time;

    private void Start()
    {
       StageInit();
    }

    private void Update()
    {
        for (int i = 1; i <= GameManager.Instance.maxHp; i++)
        {
            hearts[i - 1].SetActive(GameManager.Instance.CurHP >= i);
            blackHearts[i - 1].SetActive(GameManager.Instance.CurHP < i);
        }

        if (GameManager.Instance.curHp <= 0)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        timerText.text = $"{(int)time / 60} : {(int)time % 60}";
        time -= Time.deltaTime;
        if (time <= 0)
        {
            GameManager.Instance.stageData++;
            StageInit();
        }
    }

    public void ClickConfirmName()
    {
        if (nameInput.text.Length > 0)
        {
            GameManager.Instance.playerName = nameInput.text;
            nameInput.readOnly = true;
        }
    }

    public void ClickRanking()
    {
        if (GameManager.Instance.playerName != "")
        {
            GameManager.Instance.AddData();
            SceneManager.LoadScene("RankScene");
        }
    }

    public void ClickReTry()
    {
        GameManager.Instance.InitData();
        SceneManager.LoadScene("GameScene");
    }

    private void StageInit()
    {
        LevelManager.instance.stageInfo =
            Resources.Load($"StageInfo/{GameManager.Instance.stageData}Stage") as StageInfoSO;
        time = LevelManager.instance.stageInfo.stageTime;
        
        stageText.text = $"Stage {GameManager.Instance.stageData}";
        GameManager.Instance.curHp = GameManager.Instance.maxHp;
    }
}