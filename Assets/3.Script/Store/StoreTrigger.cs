using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTrigger : MonoBehaviour
{
    public GameObject storeUI;

    GameObject storeUIPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BoatController>() != null)
        {
            if(storeUIPrefab == null)
                storeUIPrefab = Instantiate(storeUI);
        }
    }
}
