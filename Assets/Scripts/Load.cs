using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    public Slider loadingSlider; // ������ �� ������� ��������
    public TMP_Text loadingText;     // ������ �� ����� ���������
    private float loadingDuration = 5f; // 5 ����� (300 ������)

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

            // ��������� ������� � ����� � ����������
            loadingSlider.value = progress;
            loadingText.text = Mathf.RoundToInt(progress * 100).ToString() + " %";

            yield return null; // ���� ���������� �����
        }

        // �������� ���������
        loadingSlider.value = 1f;
        loadingText.text = "100 %";
        gameObject.SetActive(false);    
    }
}
