using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Tuna : Fish
{
    protected override void Attack()
    {
        (Instantiate(Resources.Load($"Fish/Fish_Fin/{fishStat.fishName}_Fin"), bulletPoints[0]) as GameObject).transform.parent = null;
    }
}
