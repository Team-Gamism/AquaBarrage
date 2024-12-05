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

    [SerializeField] AudioClip clickAudio;
    [SerializeField] AudioClip bellAudio;
    [SerializeField] AudioClip soldAudio;

    int repairHp = 1;

    private void Start()
    {
        SetStore();
        GameManager.Instance.effectAudioSource.PlayOneShot(bellAudio);
    }

    private void OnEnable()
    {
        GameManager.UI.AddPopupUI(gameObject);
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        GameManager.UI.RemovePopupUI(gameObject);
        Time.timeScale = 1;
    }

    public void UpgradeEngine()
    {
        if (GameManager.Instance.Money >= storeSO_Engine.levelList[GameManager.Instance.engineLevel] && storeSO_Engine.levelList.Count != GameManager.Instance.engineLevel)
        {
            GameManager.Instance.Money -= storeSO_Engine.levelList[GameManager.Instance.engineLevel];
            GameManager.Instance.engineLevel++;
            SetStore();
            GameManager.Instance.effectAudioSource.PlayOneShot(soldAudio);
        }
    }

    public void UpgradeDash()
    {
        if (GameManager.Instance.Money >= storeSO_Dash.levelList[GameManager.Instance.dashLevel] && storeSO_Dash.levelList.Count != GameManager.Instance.dashLevel)
        {
            GameManager.Instance.Money -= storeSO_Engine.levelList[GameManager.Instance.dashLevel];
            GameManager.Instance.dashLevel++;
            SetStore();
            GameManager.Instance.effectAudioSource.PlayOneShot(soldAudio);
        }
    }

    public void UpgradeRill()
    {
        if (GameManager.Instance.Money >= storeSO_Rill.levelList[GameManager.Instance.rillLevel] && storeSO_Rill.levelList.Count != GameManager.Instance.rillLevel)
        {
            GameManager.Instance.Money -= storeSO_Rill.levelList[GameManager.Instance.rillLevel];
            GameManager.Instance.rillLevel++;
            SetStore();
            GameManager.Instance.effectAudioSource.PlayOneShot(soldAudio);
        }
    }

    public void Repair()
    {
        if (GameManager.Instance.Money >= 500 * repairHp)
        {
            GameManager.Instance.Money -= 500 * repairHp;
            GameManager.Instance.CurHP += repairHp;
            SetStore();
            GameManager.Instance.effectAudioSource.PlayOneShot(soldAudio);
        }
    }
    public void PlusRepairHp()
    {
        if (repairHp + GameManager.Instance.CurHP < GameManager.Instance.maxHp)
        {
            GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
            repairHp++;
            UpdateRepairPrice();
        }
    }

    public void MinusRepairHp()
    {
        if (repairHp > 1)
        {
            GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
            repairHp--;
            UpdateRepairPrice();
        }
    }


    public void UpdateRepairPrice()
    {
        repairHpText.text = $"{repairHp}";

        repairExpenseText.text = $"{500 * repairHp}";
        if(GameManager.Instance.Money >= repairHp * 500)
            repairExpenseText.color = Color.black;
        else
            repairExpenseText.color = Color.red;
    }

    public void SetStore()
    {
        moneyText.text = $"{GameManager.Instance.Money}";

        if (storeSO_Engine.levelList.Count != GameManager.Instance.engineLevel)
        {
            engineLevelText.text = $"Lv. {GameManager.Instance.engineLevel + 1}";
            engineExpenseText.text = $"{storeSO_Engine.levelList[GameManager.Instance.engineLevel]}";

            if (GameManager.Instance.Money >= storeSO_Engine.levelList[GameManager.Instance.engineLevel])
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

            if (GameManager.Instance.Money >= storeSO_Dash.levelList[GameManager.Instance.dashLevel])
                dashExpenseText.color = Color.black;
            else
                dashExpenseText.color = Color.red;
        }
        else
        {
            dashLevelText.text = "Lv. Max";
            dashExpenseText.gameObject.SetActive(false);
        }

        if (storeSO_Rill.levelList.Count != GameManager.Instance.rillLevel)
        {
            rillLevelText.text = $"Lv. {GameManager.Instance.rillLevel + 1}";
            rillExpenseText.text = $"{storeSO_Rill.levelList[GameManager.Instance.rillLevel]}";

            if (GameManager.Instance.Money >= storeSO_Rill.levelList[GameManager.Instance.rillLevel])
                rillExpenseText.color = Color.black;
            else
                rillExpenseText.color = Color.red;
        }
        else
        {
            rillLevelText.text = "Lv. Max";
            rillExpenseText.gameObject.SetActive(false);
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
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        Destroy(gameObject);
    }
}
