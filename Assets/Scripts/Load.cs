using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    public Slider loadingSlider; // Ссылка на слайдер загрузки
    public TMP_Text loadingText;     // Ссылка на текст процентов
    private float loadingDuration = 5f; // 5 минут (300 секунд)

    void Start()
    {
        StartCoroutine(LoadingCoroutine());
    }

    IEnumerator LoadingCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < loadingDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / loadingDuration;

            // Обновляем слайдер и текст с процентами
            loadingSlider.value = progress;
            loadingText.text = Mathf.RoundToInt(progress * 100).ToString() + " %";

            yield return null; // Ждем следующего кадра
        }

        // Загрузка завершена
        loadingSlider.value = 1f;
        loadingText.text = "100 %";
        gameObject.SetActive(false);    
    }
}
