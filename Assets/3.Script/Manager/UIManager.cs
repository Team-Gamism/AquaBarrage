using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public List<GameObject> uiPopupList = new List<GameObject>();

    public void RemovePopupUI(GameObject ui)
    {
        uiPopupList.Remove(ui);
    }

    public void AddPopupUI(GameObject ui)
    {
        uiPopupList.Add(ui);
    }
}
