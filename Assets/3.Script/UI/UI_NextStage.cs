using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NextStage : MonoBehaviour
{
    public Animator anim;
    public static UI_NextStage instance;
    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    public void NextStage()
    {
        anim.Play("NextStage");
    }
}
