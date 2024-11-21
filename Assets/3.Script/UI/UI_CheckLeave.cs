using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_CheckLeave : MonoBehaviour
{
   public void Leave()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}
