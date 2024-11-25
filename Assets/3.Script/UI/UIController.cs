using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    List<GameObject> uiList;

    public GameObject pause;

    private void Start()
    {
        uiList = GameManager.UI.uiPopupList;
    }

    public void OnRemoveUI()
    {
        if (uiList.Count > 0)
            Destroy(uiList[uiList.Count - 1]);
        else
        {
          if(GameManager.Instance.isPlayGame)
            Instantiate(pause);
        }
    }
}
