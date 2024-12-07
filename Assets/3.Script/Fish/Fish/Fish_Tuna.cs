using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Tuna : Fish
{
    GameObject bullet;
    protected override void Init()
    {
        base.Init();
        bullet = Resources.Load<GameObject>($"Fish/Fish_Fin/{fishStat.fishName}_Fin");
    }

    protected override void Attack()
    {
        (Instantiate(bullet, bulletPoints[0])).transform.parent = null;
    }
}
