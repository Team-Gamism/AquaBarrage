using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTutorialTrigger : MonoBehaviour
{
    [SerializeField] GameObject clearTutorialUI;

    GameObject clearTutorialUIPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (clearTutorialUIPrefab != null)
            return;
        if(other.GetComponent<BoatController>() != null)
        {
            clearTutorialUIPrefab = Instantiate(clearTutorialUI);
        }
    }
}
