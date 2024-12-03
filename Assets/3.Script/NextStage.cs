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

                //GameObject.Find("Canvas").GetComponent<UI_Game>().StageInit();


                Destroy(GameObject.Find("Store(Clone)"));

                StartCoroutine(LoadNextGameScene());
            }
    }

    IEnumerator LoadNextGameScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSeconds(2f);

        if (GameManager.Instance.stageData % 5 != 0)
            SceneManager.LoadScene("GameScene_UI_HC");
        else
            SceneManager.LoadScene("BossScene");
    }
}