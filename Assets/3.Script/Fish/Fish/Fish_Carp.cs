using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fish_Carp : Fish
{
    GameObject bullet1;
    GameObject bullet2;

    protected override void Init()
    {
        base.Init();
        bullet1 = Resources.Load<GameObject>($"Fish/Fish_Fin/{fishStat.fishName}_Fin{1}");
        bullet2 = Resources.Load<GameObject>($"Fish/Fish_Fin/{fishStat.fishName}_Fin{2}");
    }

    protected override void Attack()
    {
        int n = Random.Range(0, 2);
        switch (n)
        {
            case 1:
                Instantiate(bullet1, bulletPoints[n]).transform.parent = null;
                break;
            case 2:
                Instantiate(bullet2, bulletPoints[n]).transform.parent = null;
                break;
        }
    }
}
