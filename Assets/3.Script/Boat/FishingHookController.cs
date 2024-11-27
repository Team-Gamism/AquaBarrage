using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishingHookController : MonoBehaviour
{
    private Transform fishingOrigin;
    private Transform caughtFish;
    private float minYPos = -24.5f;

    private void Start()
    {
        fishingOrigin = GameObject.Find("FishingOrigin").transform;
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

        if (caughtFish != null)
        {
            Vector3 dir = (fishingOrigin.position - caughtFish.position).normalized;
            caughtFish.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform fish = other.transform;

        Fish fishComponent = fish.GetComponent<Fish>();
        if (fishComponent != null)
        {
            string scriptName = fishComponent.fishStat.fishName;
            Type fishScriptType = Type.GetType(scriptName);

            if (fishScriptType != null)
            {
                MonoBehaviour fishScript = fish.GetComponent(fishScriptType) as MonoBehaviour;
                if (fishScript != null)
                {
                    fishScript.enabled = false;
                }
            }
        }
        fish.SetParent(transform);
        fish.localPosition = Vector3.zero;

        caughtFish = fish;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }
}