using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    public StageInfoSO stageInfo;
    
    public bool isPausedGame = false;
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }
}
