using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Help : MonoBehaviour
{
   public void Leave()
    {
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        UI_Fade.FadeIn();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainScene");
    }
}
