using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManagerInGame : MonoBehaviour
{
    public Hero[] heros;
    public void Start()
    {
        for (int i = 0; i < heros.Length; i++)
        {
            if (i == GameManager.InstanceGame.countSkeenPlayer)
            {
                heros[i].player.SetActive(true);
            }
            else
            {
                heros[i].player.SetActive(false);
            }
            for (int j = 0; j < heros[i].weapons.Length; j++)
            {
                if (j == GameManager.InstanceGame.countSkeenGunPlayer)
                {
                    heros[i].weapons[j].SetActive(true);
                }
                else
                {
                    heros[i].weapons[j].SetActive(false);
                }
            }
        }
    }
    [Serializable]
    public struct Hero
    {
        public GameObject player;
        public GameObject[] weapons;
    }
}