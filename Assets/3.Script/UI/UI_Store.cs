using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Store : MonoBehaviour
{
    [SerializeField] Text moneyText;

    private void Start()
    {
        moneyText.text = $"{GameManager.Instance.money}";
    }
}
