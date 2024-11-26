using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Carp : Fish
{
    protected override void Attack()
    {
        int n = Random.Range(0, 2);
        (Instantiate(Resources.Load($"Fish/Fish_Fin/{fishStat.fishName}_Fin{n+1}"), bulletPoints[n]) as GameObject).transform.parent = null;
    }
}
