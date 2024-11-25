using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.isPlayGame = true;
    }

    private void OnDisable()
    {
        GameManager.Instance.isPlayGame = false;
    }
}
