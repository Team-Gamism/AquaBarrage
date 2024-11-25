using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Fade : MonoBehaviour
{
    public static Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public static void FadeIn()
    {
        anim.Play("FadeIn");
    }
}
