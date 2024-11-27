using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public StageInfoSO stageInfo;
    
    public bool isEndGame = false;
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }
}
