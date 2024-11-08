using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FishStat", menuName = "FishStat", order = 1)]
public class FishInfoSO : ScriptableObject
{
    public string fishName;
    public float speed;
    public float weight;
}
