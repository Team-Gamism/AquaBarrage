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

    private void Start()
    {
        GameManager.Instance.isPlayGame = false;
        GameManager.Instance.LoadData();
        for (int i = 0; i < nameTexts.Length; i++)
        {
            nameTexts[i].text = GameManager.Instance.playerDataList[i].playerName;
            scoreTexts[i].text = $"스테이지 {GameManager.Instance.playerDataList[i].stageData}";
        }

        if (GameManager.Instance.playerName != null)
            recentName.text = GameManager.Instance.playerName;
        else
            recentName.text = "--";
        recentScore.text = $"스테이지 {GameManager.Instance.stageData}";
    }

    public void ClickLeave()
    {
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainScene");
    }
}
