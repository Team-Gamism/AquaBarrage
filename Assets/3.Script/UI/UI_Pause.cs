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
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
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
