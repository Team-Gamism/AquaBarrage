using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageFish", menuName = "StageFish", order = 2)]
public class StageFishSO : ScriptableObject
{
    public GameObject[] stageFishes;

    public int stageTime;
}
