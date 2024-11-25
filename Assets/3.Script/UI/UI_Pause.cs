using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pause : MonoBehaviour
{
    public GameObject checkLeaveUI;

    public GameObject settingUI;
    private void OnEnable()
    {
        Time.timeScale = 0;
        GameManager.UI.AddPopupUI(gameObject);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        GameManager.UI.RemovePopupUI(gameObject);
    }

    public void Continue()
    {
        Destroy(gameObject);
    }

    public void Setting()
    {
        Instantiate(settingUI);
    }

    public void Home()
    {
        Instantiate(checkLeaveUI);
    }
}
