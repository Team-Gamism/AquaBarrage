using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Game : MonoBehaviour
{
    public InputField nameInput;
    

   
    public GameObject[] hearts;

    private void Update()
    {
        for (int i = 1; i <= GameManager.Instance.maxHp; i++)
        {
            hearts[i - 1].SetActive(GameManager.Instance.CurHP >= i);
        }

        if (GameManager.Instance.curHp <= 0)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ClickConfirmName()
    {
        if (nameInput.text.Length > 0)
        {
            GameManager.Instance.playerName = nameInput.text;
            nameInput.readOnly = true;
        }
    }

    public void GoRanking()
    {
        if (GameManager.Instance.playerName != "")
        {
            GameManager.Instance.AddData();
            SceneManager.LoadScene("RankScene");
        }
    }

}
