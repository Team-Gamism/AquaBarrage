using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public StageInfoSO stageInfo;

    public bool isPausedGame = false;
    public bool isBossCut = false;
    public bool isPrirate = false;
    public static LevelManager instance;
    public GameObject goUI;

    private void Awake()
    {
        instance = this;

        
    }

    private void Start()
    {
        if (GameObject.Find("Canvas").GetComponent<UI_Game>() != null)
            GameObject.Find("Canvas").GetComponent<UI_Game>().stageText.text = $"Stage {GameManager.Instance.stageData}";
    }
}
