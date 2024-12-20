using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Octopos : Fish, IFishable
{
    [SerializeField] GameObject Indiaink;
    protected override void Awake()
    {
        Init();
    }

    public override Transform Fished(Transform hook)
    {
        Instantiate(Indiaink,transform.position,Quaternion.identity);
        return base.Fished(hook);
    }
}
