using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_IngameHP : MonoBehaviour
{
    public GameObject[] hearts;

    private void Update()
    {
        for (int i = 1; i <= GameManager.Instance.maxHp; i++)
        {
            if (GameManager.Instance.CurHP >= i)
                hearts[i - 1].SetActive(true);
            else
                hearts[i - 1].SetActive(false);

        }

        if (GameManager.Instance.curHp <= 0)
            SceneManager.LoadScene("RankScene");
    }
}
