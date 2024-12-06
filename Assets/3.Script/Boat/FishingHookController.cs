using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishingHookController : MonoBehaviour
{
    private Transform fishingOrigin;
    private Transform caughtFish;
    private float minYPos = -24.5f;

    public Action hookAction;
    public Action getHookAction;

    bool isfalled = false;
    bool canHook;

    AudioSource audioSource;

    [SerializeField] AudioClip splashClip;

    [SerializeField] ParticleSystem fallEffect;
    [SerializeField] ParticleSystem fallEffect2;
    [SerializeField] GameObject fishEffect;
    [SerializeField] GameObject getMoneyUI;
    private void Start()
    {
        fishingOrigin = GameObject.Find("FishingOrigin").transform;
        audioSource = GetComponent<AudioSource>();
        canHook = true;
    }

    private void Update()
    {
        if (transform.position.y < minYPos)
        {
            Vector3 pos = transform.position;
            pos.y = minYPos;
            transform.position = pos;

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null && !rb.isKinematic)
            {
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
            }
        }

        if (!isfalled)
        {
            if (transform.position.y <= -2.1f)
            {
                audioSource.PlayOneShot(splashClip);
                fallEffect.gameObject.SetActive(true);
                fallEffect2.gameObject.SetActive(true);
                fallEffect2.transform.parent = null;
                isfalled = true;
            }
        }

        if (caughtFish != null)
        {
            Vector3 dir = (fishingOrigin.position - caughtFish.position).normalized;
            caughtFish.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }
    }

    private void OnDisable()
    {
        if (caughtFish != null)
        {
            Instantiate(getMoneyUI).GetComponent<UI_GetMoney>().SignGetMoney(caughtFish.GetComponent<ICanFish>().money);
            GameManager.Instance.Money += caughtFish.GetComponent<ICanFish>().money;
            getHookAction?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (caughtFish == null && canHook)
        {

            if (other.TryGetComponent<ICanFish>(out var canFish) && caughtFish == null)
            {
                Instantiate(fishEffect, transform.position, Quaternion.identity);

                caughtFish = canFish.Fished(transform);
                canHook = false;
                hookAction?.Invoke();

                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                }
            }
        }
    }
}