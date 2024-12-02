using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Eel : Fish
{
    [SerializeField] AudioClip electricyClip;
    [SerializeField] GameObject electricityEffect;
    public override Transform Fished(Transform hook)
    {
        hook.GetComponent<FishingHookController>().getHookAction += ElectricDamage;
        return base.Fished(hook);
    }

    void ElectricDamage()
    {
        GameManager.Instance.CurHP--;
        GameManager.Instance.effectAudioSource.PlayOneShot(electricyClip);
        Instantiate(electricityEffect,transform.position,Quaternion.identity);
    }
}
