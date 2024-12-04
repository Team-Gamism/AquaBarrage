using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UI_Dash : MonoBehaviour
{
    [SerializeField] Image dashCoolImage;
    [HideInInspector]
    public Transform player;
    [SerializeField] AudioClip dashCoolCLip;
    public Action canDashAction;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Move());
        
    }

    IEnumerator Move()
    {
        while (true)
        {
            yield return null;
            transform.position = player.position;
        }
    }

    public IEnumerator UpdateDash(float dashCool)
    {
        float curTime = 0;
        anim.Play("Cool");
        while (curTime < dashCool)
        {
            yield return null;  
            curTime += Time.deltaTime;
            dashCoolImage.fillAmount = curTime / dashCool;
        }
        canDashAction?.Invoke();
        anim.Play("End");
        GameManager.Instance.effectAudioSource.PlayOneShot(dashCoolCLip);
    }
}
