using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageFish", menuName = "StageFish", order = 2)]
public class StageInfoSO : ScriptableObject
{
    public List<FishType> fishSpawnList;
    public List<FishSpawnInfo> fishSpawnInfoList;


    public int stageTime;
}

[System.Serializable]
public class FishSpawnInfo
{
    public FishType fishType;
    [Range(0f, 100f)]
    public float probability;
}

