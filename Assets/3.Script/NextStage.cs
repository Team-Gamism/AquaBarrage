using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    [SerializeField] List<StageInfoSO> stageList;

    public static NextStage Instance;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.isClearStage)
            if (other.CompareTag("Player"))
            {
                LoadNextStage();
            }
    }

    public void LoadNextStage()
    {
        GameManager.Instance.stageData++;
        StartCoroutine(LoadNextGameScene());
    }

    IEnumerator LoadNextGameScene()
    {
        UI_Fade.instance.FadeIn();
        GameManager.Instance.player.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(2f);
        GameManager.Instance.isClearStage = false;

        yield return FishPreloadController.Instance
            .Preload(stageList[GameManager.Instance.stageData - 1].fishSpawnList);

        if (GameManager.Instance.stageData % 5 != 0)
            SceneManager.LoadScene("GameScene_UI_HC");
        else
            SceneManager.LoadScene("BossScene");
    }
}