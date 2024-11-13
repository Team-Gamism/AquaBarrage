using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownFish : Fish
{
    protected override void Attack()
    {
        Instantiate(Resources.Load($"Fish/Fish_Fin/{fishStat.fishName}_Fin"), bulletPoints[0]);
    }
}
