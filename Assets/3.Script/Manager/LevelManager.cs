using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public StageInfoSO stageInfo;
    
    public bool isPausedGame = false;
    public bool isBossCut = false;
    public static LevelManager instance;
    public GameObject goUI;

    private void Awake()
    {
        instance = this;
    }
}
