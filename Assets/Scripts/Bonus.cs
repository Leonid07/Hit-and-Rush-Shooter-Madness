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

    public TMP_Text hourlyBonusText; // Текст для часового бонуса

    public Button hourlyBonusButton; // Кнопка для часового бонуса

    public GameObject hourlyFG; // Графический объект для часового бонуса

    private const string HourlyBonusTimeKey = "hourly_bonus_time"; // Ключ для сохранения времени часового бонуса

    public int HourlyBonusCooldownInSeconds = 1200; // 1 час

    private void Start()
    {
        hourlyBonusButton.onClick.AddListener(ClaimHourlyBonus); // Добавление слушателя для часового бонуса
        StartCoroutine(UpdateBonusTextsRoutine());
    }

    private IEnumerator UpdateBonusTextsRoutine()
    {
        while (true)
        {
            UpdateBonusTexts();
            yield return new WaitForSeconds(0.5f); // Обновление текста каждые 0.5 секунды
        }
    }

    private void UpdateBonusTexts()
    {
        string hourlyBonusTimeStr = PlayerPrefs.GetString(HourlyBonusTimeKey, "0"); // Получение времени часового бонуса

        long hourlyBonusTime = long.Parse(hourlyBonusTimeStr); // Преобразование времени часового бонуса

        long currentTimestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

        long hourlyCooldown = hourlyBonusTime + HourlyBonusCooldownInSeconds - currentTimestamp; // Вычисление оставшегося времени для часового бонуса

        hourlyBonusText.text = FormatTimeHourly(hourlyCooldown); // Обновление текста часового бонуса

        hourlyBonusButton.interactable = hourlyCooldown <= 0; // Активность кнопки часового бонуса
    }
    private string FormatTimeHourly(long seconds) // Форматирование текста для часового бонуса
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


    private void ClaimHourlyBonus() // Метод для получения часового бонуса
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
