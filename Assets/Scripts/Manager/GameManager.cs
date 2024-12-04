using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager InstanceGame { get; private set; }

    private void Awake()
    {
        if (InstanceGame != null && InstanceGame != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstanceGame = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        LoadCountSkeenPlayer();
        LoadCountSkeenGunPlayer();
    }

    public string idCountSkeenPlayer = "countSkeenPlayer";
    public int countSkeenPlayer = 0;
    [Header("ДЛя оружия")]
    public string idCountSkeenGunPlayer = "idCountSkeenGunPlayer";
    public int countSkeenGunPlayer = 0;
    public void SaveCountSkeenPlayer()
    {
        PlayerPrefs.SetInt(idCountSkeenPlayer, countSkeenPlayer);
        PlayerPrefs.Save();
    }

    public void LoadCountSkeenPlayer()
    {
        if (PlayerPrefs.HasKey(idCountSkeenPlayer))
        {
            countSkeenPlayer = PlayerPrefs.GetInt(idCountSkeenPlayer);
        }
    }
    public void SaveCountSkeenGunPlayer()
    {
        PlayerPrefs.SetInt(idCountSkeenGunPlayer, countSkeenGunPlayer);
        PlayerPrefs.Save();
    }

    public void LoadCountSkeenGunPlayer()
    {
        if (PlayerPrefs.HasKey(idCountSkeenGunPlayer))
        {
            countSkeenGunPlayer = PlayerPrefs.GetInt(idCountSkeenGunPlayer);
        }
    }

    public float volumeSound;
}
