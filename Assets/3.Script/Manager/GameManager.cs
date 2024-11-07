using System.Collections;
using System.Collections.Generic;
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

    public Stages stageData;

    public string playerName;

    public int rankLength = 0;

    public enum Stages
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5
    }

    void InitData()
    {
        money = 0;
        stageData = Stages.Stage1;
    }

    public void EndGame()
    {
        InitData();
    }

    public void NextStage()
    {
        stageData++;
    }

    void LoadData()
    {
        rankLength = PlayerPrefs.GetInt("RankLength");
        stageData = (Stages)PlayerPrefs.GetInt("StageData");
    }

    void SaveData()
    {
        PlayerPrefs.SetInt("RankLength",rankLength++);
        PlayerPrefs.SetInt("StageData", (int)stageData);
    }
}
