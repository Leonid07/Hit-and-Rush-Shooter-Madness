using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] SpawnEnemy spawnEnemy;
    public float spawnRate = 2.0f; // Частота создания объектов (в секундах)
    public int waveCount = 2;

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (waveCount != 0)
        {
            for (int i = 0; i < spawnEnemy.maxObjects; i++)
            {
                Instantiate(spawnEnemy.objectPrefab, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnRate);
            waveCount--;
        }
    }

    [Serializable]
    struct SpawnEnemy
    {
        public GameObject objectPrefab;
        public int maxObjects;
    }
}
