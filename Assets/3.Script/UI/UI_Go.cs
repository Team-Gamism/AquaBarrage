using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Go : MonoBehaviour
{
    public Animator anim;
    public static UI_Go instance;
    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    public void Go()
    {
        anim.Play("GO!", 0);
    }
}
