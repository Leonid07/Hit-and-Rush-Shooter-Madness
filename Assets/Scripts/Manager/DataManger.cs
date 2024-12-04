using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManger : MonoBehaviour
{
    public static DataManger InstanceData { get; private set; }

    private void Awake()
    {
        if (InstanceData != null && InstanceData != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstanceData = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [Header("Параметры для уровня")]
    public TMP_Text textLevel;
    public string idCountLevel = "LevelCount";
    public int countLevel = 1;

    [Space(10)]
    public TMP_Text textCoin;
    public string idCoin = "CoinID";
    public int Coin;

    private void Start()
    {
        LoadCountLevel();
        LoadCoin();
        ApplyCoin();
    }

    public void LoadCountLevel()
    {
        if (PlayerPrefs.HasKey(idCountLevel))
        {
            countLevel = PlayerPrefs.GetInt(idCountLevel);
            ApplyTextLevel();
        }
    }
    public void SaveCountLevel()
    {
        PlayerPrefs.SetInt(idCountLevel, countLevel);
    }
    public void LoadCoin()
    {
        if (PlayerPrefs.HasKey(idCoin))
        {
            Coin = PlayerPrefs.GetInt(idCoin);
        }
    }
    public void SaveCoint()
    {
        PlayerPrefs.SetInt(idCoin, Coin);
    }
    public void ApplyCoin()
    {
        textCoin.text = Coin.ToString();
    }
    public void ApplyTextLevel()
    {
        textLevel.text = $"Level {countLevel.ToString()}";
    }
}
