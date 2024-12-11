using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public static Action parryingAcion;

    [SerializeField] Text fishCountText;
    [SerializeField] Text parryingCountText;
    [SerializeField] ParticleSystem fishParticle;
    [SerializeField] ParticleSystem parryingParticle;
    [SerializeField] GameObject clearTrigger;

    int fishCount = 0;
    int parryingCount = 0;

    Animator fishAnim;
    Animator parryingAnim;

    private void Start()
    {
        parryingAcion = SuccessParrying;
        fishCountText.text = $"{fishCount} / 2";
        parryingCountText.text = $"{parryingCount} / 2";
        fishAnim = fishCountText.GetComponent<Animator>();
        parryingAnim = parryingCountText.GetComponent<Animator>();
    }

    public void SuccessFish()
    {
        if (fishCount == 2)
            return;

        fishCount++;
        fishCountText.text = $"{fishCount} / 2";

        if (fishCount == 2)
            fishAnim.Play("Clear");
        else
            fishAnim.Play("Count");

        fishParticle.Play();

        CheckClear();
    }

    void SuccessParrying()
    {
        if (parryingCount == 2)
            return;
        parryingCount++;
        parryingCountText.text = $"{parryingCount} / 2";

        if (parryingCount == 2)
            parryingAnim.Play("Clear");
        else
            parryingAnim.Play("Count");

        parryingParticle.Play();

        CheckClear();
    }

    void CheckClear()
    {
        if(fishCount == 2 && parryingCount == 2)
        {
            clearTrigger.SetActive(true);
        }
    }
}
