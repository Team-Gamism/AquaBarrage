using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public StageInfoSO stageInfo;

    public bool isBossCut = false;
    public bool isPrirate = false;
    public static LevelManager instance;
    public GameObject goUI;

    public GameObject gameUI;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Instantiate(gameUI).GetComponent<UI_Game>().stageText.text = $"Stage {GameManager.Instance.stageData}";
    }
}
