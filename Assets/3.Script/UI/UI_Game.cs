using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class UI_Game : MonoBehaviour
{
    public InputField nameInput;
    public Text stageText;
    public Text timerText;
    public GameObject[] hearts;
    public GameObject[] blackHearts;

    public Text gameOverStageText;
    public Text resultStageText;

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
            LevelManager.instance.isPausedGame = true;
            
            transform.GetChild(1).gameObject.SetActive(true); //GameOver Panel
            gameOverStageText.text = $"최종기록 : Stage {GameManager.Instance.stageData}";
        }

        if (!LevelManager.instance.isPausedGame)
        {
            timerText.text = $"{(int)time / 60} : {(int)time % 60}";
            time -= Time.deltaTime;
        }

        
        if (time <= 0 && !GameManager.Instance.isClearStage)
        {
            GameManager.Instance.isClearStage = true;
            
            Transform trans = Instantiate(Resources.Load<GameObject>("Store")).transform;
            trans.rotation = Quaternion.Euler(0f, 90f, 0f);
            trans.GetChild(0).position = trans.position+new Vector3(0.215f, 0.715f, 0.1656f);
            
            LevelManager.instance.isPausedGame = true;

            StartCoroutine(GO());
        }
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(8f);
        UI_Go.instance.Go();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.stageData++;
        StageInit();
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
            StartCoroutine(LoadRankScene());
        }
    }
    
    IEnumerator LoadRankScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("RankScene");
    }

    public void ClickReTry()
    {
        GameManager.Instance.InitData();
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameScene");
    }

    public void StageInit()
    {
        LevelManager.instance.stageInfo =
            Resources.Load($"StageInfo/{GameManager.Instance.stageData}Stage") as StageInfoSO;
        time = LevelManager.instance.stageInfo.stageTime;
        GameManager.Instance.isClearStage = false;
        LevelManager.instance.isPausedGame = false;

        GameObject.Find("Boat").transform.position = new Vector3(0f, -2f, 0f);

        stageText.text = $"Stage {GameManager.Instance.stageData}";
    }
}