using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GetMoney : MonoBehaviour
{
    [SerializeField] Text moneyText;

    private void Start()
    {
        transform.position = GameManager.Instance.player.transform.position;
    }

    public void SignGetMoney(int money)
    {
        moneyText.text = "+"+ money.ToString();
    }

    void Disappoint()
    {
        Destroy(gameObject);
    }
}
