using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTrigger : MonoBehaviour
{
    public GameObject storeUI;

    GameObject storeUIPrefab;

    [SerializeField] GameObject interactUI;

    bool canInteract;

    private void Start()
    {
        interactUI.SetActive(false);
    }

    public void OnInteract()
    {
        if (!canInteract)
            return;

        if (storeUIPrefab == null)
            storeUIPrefab = Instantiate(storeUI);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BoatController>() != null)
        {
            interactUI.SetActive(true);
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BoatController>() != null)
        {
            interactUI.SetActive(false);
            canInteract = false;
        }
    }
}
