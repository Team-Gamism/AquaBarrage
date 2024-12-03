using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Fade : MonoBehaviour
{
    public Animator anim;
    public static UI_Fade instance;


    private void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
        GameManager.Instance.audioMixer.DOSetFloat("music",GameManager.Instance.musicAmount,2);
        anim.Play("FadeOut");
    }

    public void FadeIn()
    {
        GameManager.Instance.audioMixer.GetFloat("music", out GameManager.Instance.musicAmount);
        GameManager.Instance.audioMixer.DOSetFloat("music", -80, 3);
        anim.Play("FadeIn");
    }
    public void FadeOut()
    {
        Time.timeScale = 1f;
        anim.Play("FadeOut");
    }
}
