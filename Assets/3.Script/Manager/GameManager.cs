using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } set { instance = value; } }

    static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            InitData();
            LoadData();
            DontDestroyOnLoad(gameObject);
        }
    }

    public int money;

    public int stageData;

    public string playerName;

    public List<PlayerData> playerDataList = new List<PlayerData>();


    public void InitData()
    {
        money = 0;
        stageData = 1;
        playerName = "";
    }

    public void EndGame()
    {
        InitData();
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
        if (PlayerPrefs.HasKey("RankLength"))
        {
            for (int i = 0; i < 5; i++)
            {
                playerDataList.Add(new PlayerData());
                playerDataList[i].stageData =  PlayerPrefs.GetInt($"StageData{i}");
                playerDataList[i].playerName = PlayerPrefs.GetString($"NameData{i}");
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                playerDataList.Add(new PlayerData());
                playerDataList[i].stageData = 0;
                playerDataList[i].playerName = "--";
            }
        }
    }

    public void AddData()
    {
        if (playerDataList[4].stageData < stageData)
        {
            playerDataList[4].stageData = stageData;
            playerDataList[4].playerName = playerName;
            SortData();
        }
    }

    public void SortData()
    {
        playerDataList = playerDataList.OrderByDescending(obj => obj.stageData).ToList();
    }

    
}
