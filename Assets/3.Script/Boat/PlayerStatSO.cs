using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatSO", menuName = "SO/PlayerStatSO")]
public class PlayerStatSO : ScriptableObject
{
    public List<float> valueList = new List<float>();
}
