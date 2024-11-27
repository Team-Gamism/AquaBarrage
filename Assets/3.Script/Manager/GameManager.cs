using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } set { instance = value; } }

    static GameManager instance;

    UIManager ui = new();

    public static UIManager UI { get { return Instance.ui; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitData();
            LoadData();
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool isPlayGame;
    public bool isDash;

    public int money;
    public int fishCount;

    public int stageData;

    public int maxHp;
    public int curHp;
    public int CurHP
    {
        get => curHp;
        set
        {
            curHp = Mathf.Clamp(value, 0, maxHp);
        }
    }

    public string playerName;

    public List<PlayerData> playerDataList = new List<PlayerData>();


    public int engineLevel;
    public int rillLevel;
    public int fishLevel;


    public void InitData()
    {
        money = 0;
        stageData = 1;
        playerName = "";
        engineLevel = 0;
        fishLevel = 0;
        rillLevel = 0;

        curHp = maxHp;
    }

    public void EndGame()
    {
        InitData();
        isPlayGame = false;
    }

    public void NextStage()
    {
        stageData++;
    }

    public class PlayerData
    {
        public string playerName;
        public int stageData;
    }

    public void LoadData()
    {
            for (int i = 0; i < 5; i++)
            {
                playerDataList.Add(new PlayerData());
                playerDataList[i].stageData = PlayerPrefs.GetInt($"StageData{i}");
                playerDataList[i].playerName = PlayerPrefs.GetString($"NameData{i}");
            }
    }

    public void AddData()
    {
        if (playerDataList[4].stageData < stageData)
        {
            playerDataList[4].stageData = stageData;
            playerDataList[4].playerName = playerName;
            Debug.Log(playerName);
            SortData();
        }
    }

    public void SortData()
    {
        playerDataList = playerDataList.OrderByDescending(obj => obj.stageData).ToList();

        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt($"StageData{i}", playerDataList[i].stageData);
            PlayerPrefs.SetString($"NameData{i}", playerDataList[i].playerName);
        }
    }

    public GameObject uiController;
}
