using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelGameManager : MonoBehaviour
{
    public Timer timer;
    public Button buttonPause;

    public GameObject pausePanel;

    public Button buttonRestart;
    public Button buttonHome;
    public Button buttonBack;

    [Header("Панель проигрыша")]
    public GameObject panelLose;
    public Button buttonRestartLose;
    public Button buttonHomeLose;

    [Header("Панель выйгрыша")]
    public GameObject panelWin;
    public Button buttonNextLevel;
    public Button buttonHomeWin;

    public void Start()
    {
        buttonPause.onClick.AddListener(()=> { pausePanel.SetActive(true); timer.StopCountdownCoroutine(); });

        buttonRestart.onClick.AddListener(Restart);
        buttonRestartLose.onClick.AddListener(Restart);
        buttonHome.onClick.AddListener(Home);
        buttonHomeLose.onClick.AddListener(Home);
        buttonHomeWin.onClick.AddListener(HomeWin);
        buttonBack.onClick.AddListener(Back);

        buttonNextLevel.onClick.AddListener(NextLevel);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        scene++;
        DataManger.InstanceData.countLevel++;
        DataManger.InstanceData.SaveCountLevel();
        SceneManager.LoadScene(scene);
    }
    public void Home()
    {
        SceneManager.LoadScene(0);
        CanvasManager.InstanceMainCanvas.gameObject.SetActive(true);
    }
    public void HomeWin()
    {
        SceneManager.LoadScene(0);
        int scene = SceneManager.GetActiveScene().buildIndex;
        scene++;

        //DataManger.InstanceData.countLevel++;
        //DataManger.InstanceData.SaveCountLevel();

        CanvasManager.InstanceMainCanvas.gameObject.SetActive(true);
    }
    public void Back()
    {
        pausePanel.SetActive(false);
        timer.StartCountdownCoroutine();
    }
    public void ActivePanelLose()
    {
        panelLose.SetActive(true);
        timer.StopCountdownCoroutine();
    }
    public void ActivePanelWin()
    {
        panelWin.SetActive(true);
        timer.StopCountdownCoroutine();
        DataManger.InstanceData.countLevel++;
        DataManger.InstanceData.SaveCountLevel();
        DataManger.InstanceData.Coin += 50;
        DataManger.InstanceData.SaveCoint();
    }
}