using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.isClearStage)
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.stageData++;
                GameObject.Find("Canvas").GetComponent<UI_Game>().StageInit();
                UI_NextStage.instance.NextStage();

                Destroy(GameObject.Find("Store(Clone)"));
            }
    }
}