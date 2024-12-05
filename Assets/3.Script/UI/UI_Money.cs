using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Money : MonoBehaviour
{
    [SerializeField] Text moneyText;
    private void Start()
    {
        GameManager.Instance.getMoneyAction = SetMoney;
    }

    void SetMoney(int totalMoney)
    {
        moneyText.text = totalMoney.ToString();
    }
}
