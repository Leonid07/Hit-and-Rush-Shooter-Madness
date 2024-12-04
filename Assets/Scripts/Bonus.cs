using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    public static Bonus InstanceBonus { get; private set; }

    private void Awake()
    {
        if (InstanceBonus != null && InstanceBonus != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstanceBonus = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public AddRewardBonus rewardBonus;

    public TMP_Text hourlyBonusText; // ����� ��� �������� ������

    public Button hourlyBonusButton; // ������ ��� �������� ������

    public GameObject hourlyFG; // ����������� ������ ��� �������� ������

    private const string HourlyBonusTimeKey = "hourly_bonus_time"; // ���� ��� ���������� ������� �������� ������

    public int HourlyBonusCooldownInSeconds = 1200; // 1 ���

    private void Start()
    {
        hourlyBonusButton.onClick.AddListener(ClaimHourlyBonus); // ���������� ��������� ��� �������� ������
        StartCoroutine(UpdateBonusTextsRoutine());
    }

    private IEnumerator UpdateBonusTextsRoutine()
    {
        while (true)
        {
            UpdateBonusTexts();
            yield return new WaitForSeconds(0.5f); // ���������� ������ ������ 0.5 �������
        }
    }

    private void UpdateBonusTexts()
    {
        string hourlyBonusTimeStr = PlayerPrefs.GetString(HourlyBonusTimeKey, "0"); // ��������� ������� �������� ������

        long hourlyBonusTime = long.Parse(hourlyBonusTimeStr); // �������������� ������� �������� ������

        long currentTimestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

        long hourlyCooldown = hourlyBonusTime + HourlyBonusCooldownInSeconds - currentTimestamp; // ���������� ����������� ������� ��� �������� ������

        hourlyBonusText.text = FormatTimeHourly(hourlyCooldown); // ���������� ������ �������� ������

        hourlyBonusButton.interactable = hourlyCooldown <= 0; // ���������� ������ �������� ������
    }
    private string FormatTimeHourly(long seconds) // �������������� ������ ��� �������� ������
    {
        if (seconds <= 0)
        {
            hourlyFG.SetActive(false);
            return "";
        }
        hourlyFG.SetActive(true);
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }


    private void ClaimHourlyBonus() // ����� ��� ��������� �������� ������
    {
        long currentTimestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

        int randCoin = UnityEngine.Random.Range(10, 70);
        DataManger.InstanceData.Coin += randCoin;

        rewardBonus.ActivePanel();
        rewardBonus.textCoin.text = $"+{randCoin}";

        DataManger.InstanceData.ApplyCoin();

        PlayerPrefs.SetString(HourlyBonusTimeKey, currentTimestamp.ToString());
        PlayerPrefs.Save();

        Debug.Log("Hourly Bonus Claimed!");
        Debug.Log($"New Hourly Bonus Time: {currentTimestamp}");
    }
}
