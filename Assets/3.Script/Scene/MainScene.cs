using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    public GameObject uiController;

    private void Start()
    {
        GameManager.Instance.isPlayGame = false;
        if (GameManager.Instance.uiController == null)
        {
            GameManager.Instance.uiController = Instantiate(uiController);
            DontDestroyOnLoad(GameManager.Instance.uiController);
        }
    }
}
