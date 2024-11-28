using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Store : MonoBehaviour
{
    [SerializeField] Text moneyText;
    [SerializeField] Text engineLevelText;
    [SerializeField] Text dashLevelText;
    [SerializeField] Text rillLevelText;
    [SerializeField] Text boatHpText;
    [SerializeField] Text engineExpenseText;
    [SerializeField] Text dashExpenseText;
    [SerializeField] Text rillExpenseText;
    [SerializeField] Text boatExpenseText;
    [SerializeField] Text repairExpenseText;
    [SerializeField] Text repairHpText;

    [SerializeField] GameObject repairImage;

    [SerializeField] StoreSO storeSO_Dash;
    [SerializeField] StoreSO storeSO_Engine;
    [SerializeField] StoreSO storeSO_Rill;

    int repairHp = 1;

    private void Start()
    {
        SetStore();
    }

    private void OnEnable()
    {
        GameManager.UI.AddPopupUI(gameObject);
    }

    private void OnDisable()
    {
        GameManager.UI.RemovePopupUI(gameObject);
    }

    public void UpgradeEngine()
    {
        if (GameManager.Instance.money >= storeSO_Engine.levelList[GameManager.Instance.engineLevel] && storeSO_Engine.levelList.Count != GameManager.Instance.engineLevel)
        {
            GameManager.Instance.money -= storeSO_Engine.levelList[GameManager.Instance.engineLevel];
            GameManager.Instance.engineLevel++;
            SetStore();
        }
    }

    public void UpgradeDash()
    {
        if (GameManager.Instance.money >= storeSO_Dash.levelList[GameManager.Instance.dashLevel] && storeSO_Dash.levelList.Count != GameManager.Instance.dashLevel)
        {
            GameManager.Instance.money -= storeSO_Engine.levelList[GameManager.Instance.dashLevel];
            GameManager.Instance.dashLevel++;
            SetStore();
        }
    }

    public void UpgradeRill()
    {
        if (GameManager.Instance.money >= storeSO_Rill.levelList[GameManager.Instance.rillLevel] && storeSO_Rill.levelList.Count != GameManager.Instance.rillLevel)
        {
            GameManager.Instance.money -= storeSO_Rill.levelList[GameManager.Instance.rillLevel];
            GameManager.Instance.rillLevel++;
            SetStore();
        }
    }

    public void Repair()
    {
        if (GameManager.Instance.money >= 1000 * repairHp)
        {
            GameManager.Instance.money -= 1000 * repairHp;
            GameManager.Instance.CurHP += repairHp;
            SetStore();
        }
    }
    public void PlusRepairHp()
    {
        if (repairHp + GameManager.Instance.CurHP < GameManager.Instance.maxHp)
        {
            repairHp++;
            UpdateRepairPrice();
        }
    }

    public void MinusRepairHp()
    {
        if (repairHp > 1)
        {
            repairHp--;
            UpdateRepairPrice();
        }
    }


    public void UpdateRepairPrice()
    {
        repairHpText.text = $"{repairHp}";

        repairExpenseText.text = $"{1000 * repairHp}";
        if(GameManager.Instance.money >= repairHp * 1000)
            repairExpenseText.color = Color.black;
        else
            repairExpenseText.color = Color.red;
    }

    public void SetStore()
    {
        moneyText.text = $"{GameManager.Instance.money}";

        if (storeSO_Engine.levelList.Count != GameManager.Instance.engineLevel)
        {
            engineLevelText.text = $"Lv. {GameManager.Instance.engineLevel + 1}";
            engineExpenseText.text = $"{storeSO_Engine.levelList[GameManager.Instance.engineLevel]}";

            if (GameManager.Instance.money >= storeSO_Engine.levelList[GameManager.Instance.engineLevel])
                engineExpenseText.color = Color.black;
            else
                engineExpenseText.color = Color.red;
        }
        else
        {
            engineLevelText.text = "Lv. Max";
            engineExpenseText.gameObject.SetActive(false);
        }

        if (storeSO_Dash.levelList.Count != GameManager.Instance.dashLevel)
        {
            dashLevelText.text = $"Lv. {GameManager.Instance.dashLevel + 1}";
            dashExpenseText.text = $"{storeSO_Dash.levelList[GameManager.Instance.dashLevel]}";

            if (GameManager.Instance.money >= storeSO_Dash.levelList[GameManager.Instance.dashLevel])
                dashExpenseText.color = Color.black;
            else
                dashExpenseText.color = Color.red;
        }
        else
        {
            dashLevelText.text = "Lv. Max";
            dashLevelText.gameObject.SetActive(false);
        }

        if (storeSO_Rill.levelList.Count != GameManager.Instance.rillLevel)
        {
            rillLevelText.text = $"Lv. {GameManager.Instance.rillLevel + 1}";
            rillExpenseText.text = $"{storeSO_Rill.levelList[GameManager.Instance.rillLevel]}";

            if (GameManager.Instance.money >= storeSO_Rill.levelList[GameManager.Instance.rillLevel])
                rillExpenseText.color = Color.black;
            else
                rillExpenseText.color = Color.red;
        }
        else
        {
            rillLevelText.text = "Lv. Max";
            rillLevelText.gameObject.SetActive(false);
        }


        boatHpText.text = $"{GameManager.Instance.CurHP}/{GameManager.Instance.maxHp}";

        repairHp = Mathf.Clamp(repairHp, 1, GameManager.Instance.maxHp - GameManager.Instance.CurHP);
        UpdateRepairPrice();

        if (GameManager.Instance.maxHp == GameManager.Instance.CurHP)
        {
            repairImage.SetActive(false);
        }
    }

    public void Leave()
    {
        Destroy(gameObject);
    }
}
