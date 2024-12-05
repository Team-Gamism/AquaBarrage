using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Rank : MonoBehaviour
{
    public Text[] nameTexts;
    public Text[] scoreTexts;

    public Text recentName;
    public Text recentScore;

    [SerializeField] AudioClip clickAudio;

    private void Start()
    {
        GameManager.Instance.isPlayGame = false;
        GameManager.Instance.LoadData();
        for (int i = 0; i < nameTexts.Length; i++)
        {
            nameTexts[i].text = GameManager.Instance.playerDataList[i].playerName;
            scoreTexts[i].text = $"스테이지 {GameManager.Instance.playerDataList[i].stageData}";
        }

        if (GameManager.Instance.playerName != "")
        {
            recentName.text = GameManager.Instance.playerName;
            recentScore.text = $"스테이지 {GameManager.Instance.stageData}";
        }
        else
        {
            recentName.text = "--";
            recentScore.text = $"스테이지 0";
        }
    }

    public void ClickLeave()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainScene");
    }

    public void OnInitRank()
    {
        GameManager.Instance.InitRank();
        for (int i = 0; i < nameTexts.Length; i++)
        {
            nameTexts[i].text = "";
            scoreTexts[i].text = $"스테이지 0";
        }

        recentName.text = "--";
        recentScore.text = $"스테이지 0";

    }
}
