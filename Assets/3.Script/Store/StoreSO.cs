using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="StoreSO",menuName ="SO/StoreSO")]
public class StoreSO : ScriptableObject
{
    public List<int> levelList = new List<int>();
}
