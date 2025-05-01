using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

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
    public bool isClearStage;
    public bool isExplosionDamage;


    public Action<int> getMoneyAction;

    public int Money { get { return money; } set { getMoneyAction?.Invoke(value); money = value; } }

    int money;
    public int fishCount;

    public int stageData;

    public int maxHp;

    public Action hitEvent;
    public Action explosionHitEvent;


    public Transform player;

    public GameObject uiController;

    public AudioSource effectAudioSource;

    public AudioMixer audioMixer;

    public bool isChangeScene;

    public float musicAmount;
    public bool isNoDamage;

    public int curHp;
    public int CurHP
    {
        get => curHp;
        set
        {
            if (isNoDamage)
                return;

            if (curHp > value)
            {
                hitEvent?.Invoke();
                curHp = Mathf.Clamp(value, 0, maxHp);
            }
        }
    }
    public string playerName;

    public List<PlayerData> playerDataList = new List<PlayerData>();

    public int engineLevel;
    public int rillLevel;
    public int dashLevel;

    public void InitData()
    {
        money = 0;
        stageData = 1;
        playerName = "";
        engineLevel = 0;
        dashLevel = 0;
        rillLevel = 0;
        fishCount = 0;
        isPlayGame = true;
        isClearStage = false;
        CurHP = maxHp;
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
        public int catchFishCount;
    }

    public void LoadData()
    {
        for (int i = 0; i < 5; i++)
        {
            playerDataList.Add(new PlayerData());
            playerDataList[i].stageData = PlayerPrefs.GetInt($"StageData{i}");
            playerDataList[i].playerName = PlayerPrefs.GetString($"NameData{i}");
            playerDataList[i].catchFishCount = PlayerPrefs.GetInt($"CatchData{i}");
        }
    }

    public void AddData()
    {
        if (playerDataList[4].stageData < stageData)
        {
            playerDataList[4].stageData = stageData;
            playerDataList[4].playerName = playerName;
            playerDataList[4].catchFishCount = fishCount;
            Debug.Log(playerName);
            SortData();
        }
        else if (playerDataList[4].stageData == stageData)
        {
            if (playerDataList[4].catchFishCount < fishCount)
            {
                playerDataList[4].stageData = stageData;
                playerDataList[4].playerName = playerName;
                playerDataList[4].catchFishCount = fishCount;
                SortData();
            }
        }
    }

    public void SortData()
    {
        playerDataList = playerDataList.OrderByDescending(obj => obj.catchFishCount).ToList();
        playerDataList = playerDataList.OrderByDescending(obj => obj.stageData).ToList();

        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt($"StageData{i}", playerDataList[i].stageData);
            PlayerPrefs.SetString($"NameData{i}", playerDataList[i].playerName);
            PlayerPrefs.SetInt($"CatchData{i}", playerDataList[i].catchFishCount);
        }
    }

    public void InitRank()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt($"StageData{i}", 0);
            PlayerPrefs.SetString($"NameData{i}", "");
            PlayerPrefs.SetInt($"CatchData{i}", 0);
        }
    }
}
