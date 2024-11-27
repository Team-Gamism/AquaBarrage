using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Fade : MonoBehaviour
{
    public Animator anim;
    public static UI_Fade instance;
    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
        anim.Play("FadeOut");
    }

    public void FadeIn()
    {
        anim.Play("FadeIn");
    }
}
