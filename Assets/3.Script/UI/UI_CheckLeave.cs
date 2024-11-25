using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_CheckLeave : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.UI.AddPopupUI(gameObject);
    }

    private void OnDisable()
    {
        GameManager.UI.RemovePopupUI(gameObject);
    }

    public void Leave()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}
