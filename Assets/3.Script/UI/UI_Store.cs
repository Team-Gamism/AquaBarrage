using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Store : MonoBehaviour
{
    [SerializeField] Text moneyText;
    [SerializeField] Text engineLevelText;
    [SerializeField] Text fishLevelText;
    [SerializeField] Text rillLevelText;
    [SerializeField] Text boatHpText;
    [SerializeField] Text engineExpenseText;
    [SerializeField] Text fishExpenseText;
    [SerializeField] Text rillExpenseText;
    [SerializeField] Text boatExpenseText;
    [SerializeField] Text repairExpenseText;
    [SerializeField] Text repairHpText;

    [SerializeField] GameObject repairImage;

    [SerializeField] StoreSO storeSO_Fish;
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

    public void UpgradeFish()
    {
        if (GameManager.Instance.money >= storeSO_Fish.levelList[GameManager.Instance.fishLevel] && storeSO_Fish.levelList.Count != GameManager.Instance.fishLevel)
        {
            GameManager.Instance.money -= storeSO_Engine.levelList[GameManager.Instance.fishLevel];
            GameManager.Instance.fishLevel++;
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
            engineLevelText.text = $"Lv. {GameManager.Instance.engineLevel + 1}";
        else
            engineLevelText.text = "Lv. Max";

        if (storeSO_Fish.levelList.Count != GameManager.Instance.fishLevel)
            fishLevelText.text = $"Lv. {GameManager.Instance.fishLevel + 1}";
        else
            fishLevelText.text = "Lv. Max";

        if (storeSO_Rill.levelList.Count != GameManager.Instance.rillLevel)
            rillLevelText.text = $"Lv. {GameManager.Instance.rillLevel + 1}";
        else
            rillLevelText.text = "Lv. Max";

        engineExpenseText.text = $"{storeSO_Engine.levelList[GameManager.Instance.engineLevel]}";
        fishExpenseText.text = $"{storeSO_Fish.levelList[GameManager.Instance.fishLevel]}";
        rillExpenseText.text = $"{storeSO_Rill.levelList[GameManager.Instance.rillLevel]}";

        if (GameManager.Instance.money >= storeSO_Engine.levelList[GameManager.Instance.engineLevel])
            engineExpenseText.color = Color.black;
        else
            engineExpenseText.color = Color.red;

        if (GameManager.Instance.money >= storeSO_Fish.levelList[GameManager.Instance.fishLevel])
            fishExpenseText.color = Color.black;
        else
            fishExpenseText.color = Color.red;

        if (GameManager.Instance.money >= storeSO_Rill.levelList[GameManager.Instance.rillLevel])
            rillExpenseText.color = Color.black;
        else
            rillExpenseText.color = Color.red;

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
