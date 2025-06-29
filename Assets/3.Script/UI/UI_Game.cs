using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Game : MonoBehaviour
{
    public InputField gameOverNameInput;
    public InputField resultNameInput;
    public Text stageText;
    public Text timerText;
    public Text curCaughtFishText;
    public GameObject[] hearts;
    public GameObject[] blackHearts;

    public Text gameOverStageText;
    public Text resultStageText;

    GameObject store;
    [SerializeField] AudioClip clickAudio;

    private float time;

    private void Start()
    {
        GameManager.Instance.isPlayGame = true;
        store = Resources.Load<GameObject>("Store");

        if(GameManager.Instance.stageData == 1)
            GameManager.Instance.InitData();
        StageInit();
        StartCoroutine(UpdateGame());
    }

    IEnumerator UpdateGame()
    {
        while (true)
        {
            yield return null;
            for (int i = 1; i <= GameManager.Instance.maxHp; i++)
            {
                hearts[i - 1].SetActive(GameManager.Instance.CurHP >= i);
                blackHearts[i - 1].SetActive(GameManager.Instance.CurHP < i);
            }

            if (GameManager.Instance.curHp <= 0 && !LevelManager.instance.isBossCut)
            {

                transform.GetChild(1).gameObject.SetActive(true); //GameOver Panel
                gameOverStageText.text = $"최종기록 : Stage {GameManager.Instance.stageData}";
            }

            if (!GameManager.Instance.isClearStage)
            {
                if (GameManager.Instance.stageData % 5 != 0)
                    timerText.text = $"{(int)time / 60} : {(int)time % 60}";
                else
                    timerText.text = "∞";
               time -= Time.deltaTime;
            }


            if (time <= 0 && !GameManager.Instance.isClearStage)
            {
                GameManager.Instance.isClearStage = true;

                Transform trans = Instantiate(store).transform;
                trans.rotation = Quaternion.Euler(0f, 90f, 0f);
                trans.GetChild(0).position = trans.position + new Vector3(0.215f, 0.715f, 0.1656f);
                StartCoroutine(GO());
            }

            if (LevelManager.instance.isBossCut)
            {
                transform.GetChild(2).gameObject.SetActive(true); //Result Panel
                resultStageText.text = $"최종기록 : Stage {GameManager.Instance.stageData}";
                curCaughtFishText.text = $"잡은 물고기 수 : {GameManager.Instance.fishCount}마리";
            }
        }
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(5f);
        LevelManager.instance.goUI = Instantiate(Resources.Load("UI_GO"), transform) as GameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.stageData++;
        StageInit();
    }

    public void GameOverClickConfirmName()
    {
        if (gameOverNameInput.text.Length > 0)
        {
            GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
            GameManager.Instance.playerName = gameOverNameInput.text;
            gameOverNameInput.readOnly = true;
        }
    }
    public void ResultClickConfirmName()
    {
        if (resultNameInput.text.Length > 0)
        {
            GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
            GameManager.Instance.playerName = resultNameInput.text;
            resultNameInput.readOnly = true;
        }
    }

    public void ClickRanking()
    {
        if (GameManager.Instance.playerName != "")
        {
            GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
            GameManager.Instance.AddData();
            StartCoroutine(LoadRankScene());
        }
    }

    IEnumerator LoadRankScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("RankScene");
    }

    public void ClickReTry()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        GameManager.Instance.InitData();
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameScene_UI_HC");
    }



    public void StageInit()
    {
        LevelManager.instance.stageInfo =
            Resources.Load($"StageInfo/{GameManager.Instance.stageData}Stage") as StageInfoSO;

        time = LevelManager.instance.stageInfo.stageTime;
        GameManager.Instance.isClearStage = false;
        GameManager.Instance.isChangeScene = false;
        if (GameManager.Instance.stageData > 1)
            UI_NextStage.instance.NextStage();
    }
}