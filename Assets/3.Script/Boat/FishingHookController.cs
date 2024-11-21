using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHookController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            Transform fish = other.transform;
            Transform mouth = fish.Find("Mouth");

            if (mouth != null)
            {
                switch (fish.name)
                {
                    case "ClownFish":
                        fish.GetComponent<ClownFish>().enabled = false;
                        break;
                }
                Vector3 mouthPos = mouth.position;

                mouth.SetParent(transform);
                mouth.localPosition = Vector3.zero;

                fish.SetParent(transform);
                fish.position = mouthPos;

                Vector3 dir = (transform.position - fish.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.up);
                fish.rotation = lookRotation;

                fish.localPosition = Vector3.zero;
            }
        }
    }
}