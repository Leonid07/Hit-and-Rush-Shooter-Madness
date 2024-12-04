using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public PanelGameManager panelGameManager;

    public AudioSource audioSource;

    public float countdownTime = 120f; // ����� ��������� �������
    public TMP_Text timerText; // ������ �� ��������� TMP_Text
    public Slider timerSlider; // ������ �� ��������� Slider

    public TMP_Text text;
    public int minBuck = 0;
    public int maxBuck = 10;
    public GameObject[] box;

    [Header("������ �� ��������")]
    public GameObject[] indicatorSlider;

    private Coroutine countdownCoroutine; // ������ �� ��������
    public float timeRemaining; // ���������� �����

    private void Start()
    {
        panelGameManager = GetComponent<PanelGameManager>();

        audioSource = GameObject.Find("AudioMusic").GetComponent<AudioSource>();
        audioSource.volume = GameManager.InstanceGame.volumeSound;

        maxBuck = box.Length;
        text.text = $"Barrel {minBuck}/{maxBuck}";
        timeRemaining = countdownTime;
        countdownCoroutine = StartCoroutine(StartCountdown(countdownTime));
    }

    public void StartCountdownCoroutine()
    {
        if (countdownCoroutine != null) // ���������, �������� �� ��������
        {
            StopCoroutine(countdownCoroutine); // ������������� ������� ��������
        }

        //timeRemaining = countdownTime; // ������������� ���������� �����
        countdownCoroutine = StartCoroutine(StartCountdown(timeRemaining));
    }

    public void StopCountdownCoroutine()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null; // ������� ������ �� ��������
        }
    }

    IEnumerator CheckCor()
    {
        yield return new WaitForSeconds(0.2f);
        minBuck = 0;
        for (int i = 0; i < box.Length; i++)
        {
            if (box[i] == null)
            {
                minBuck++;
                text.text = $"Barrel {minBuck}/{maxBuck}";
            }
        }
        if (minBuck == box.Length)
        {
            panelGameManager.ActivePanelWin();
        }
    }

    public void Check()
    {
        StartCoroutine(CheckCor());
    }

    private IEnumerator StartCountdown(float duration)
    {
        timerSlider.maxValue = countdownTime;
        timerSlider.value = duration; // ���������� ���������� ��������

        while (timeRemaining > 0)
        {
            timerText.text = Mathf.Ceil(timeRemaining).ToString();
            timerSlider.value = timeRemaining;

            // ������� ������ � ����������� �� �������
            switch (timerSlider.value)
            {
                case 14:
                    indicatorSlider[0].SetActive(false);
                    break;
                case 54:
                    indicatorSlider[1].SetActive(false);
                    break;
                case 96:
                    indicatorSlider[2].SetActive(false);
                    break;
            }
            timeRemaining -= Time.deltaTime; // ��������� ���������� �����
            yield return null; // ���� �� ���������� �����
        }
        panelGameManager.ActivePanelLose();
        timerText.text = "0";
        timerSlider.value = 0;
        Debug.Log("����� �����!");
    }
}
