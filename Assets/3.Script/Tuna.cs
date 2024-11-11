using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuna : Fish
{
    protected override void Attack()
    {
        (Instantiate(Resources.Load($"Fish/Fish_Fin/{fishInfo.fishName}_Fin"), bulletPoints[0]) as GameObject).transform.parent = null;
    }
}
