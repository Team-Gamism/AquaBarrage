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
        Vector3 movementDirection = transform.position - lastPosition;

        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            Transform fish = other.transform;

            string scriptName = fish.name;
            Type fishScriptType = Type.GetType(scriptName);

            if (fishScriptType != null)
            {
                MonoBehaviour fishScript = fish.GetComponent(fishScriptType) as MonoBehaviour;
                if (fishScript != null)
                {
                    fishScript.enabled = false;
                }

                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                }

                fish.SetParent(transform);

                fish.localPosition = Vector3.zero;

                Vector3 movementDirection = (transform.position - lastPosition).normalized;

                if (movementDirection.sqrMagnitude > 0.001f)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                    fish.rotation = lookRotation;
                }
            }
        }
    }
}