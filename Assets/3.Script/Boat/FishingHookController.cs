using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHookController : MonoBehaviour
{
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        lastPosition = transform.position;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICanFish>(out var canFish))
        {
            canFish.Fished(transform, lastPosition);
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
        }
    }
}