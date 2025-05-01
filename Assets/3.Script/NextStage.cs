using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.isClearStage)
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.stageData++;

                StartCoroutine(LoadNextGameScene());
            }
    }

    IEnumerator LoadNextGameScene()
    {
        UI_Fade.instance.FadeIn();
        GameManager.Instance.player.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(2f);
        GameManager.Instance.isClearStage = false;
        if (GameManager.Instance.stageData % 5 != 0)
            SceneManager.LoadScene("GameScene_UI_HC");
        else
            SceneManager.LoadScene("BossScene");
    }
}