using System;
using System.Collections;
using System.Diagnostics;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Rendering;

public class Emy_Spawner : MonoBehaviour
{
    [SerializeField] GameObject ShooterPrefab;
    [SerializeField] GameObject AsteroidPrefab;
    [SerializeField] GameObject UFOSPrefab;
    [SerializeField] float SpawnInterval;
    [SerializeField] private float NextSpawn;
    [SerializeField] float Ybound;
    [SerializeField] float currentScore;
    private float XBound = 9f;
    public int currentRound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = ScoreSystem.ScoreValue;
        if (currentScore >= 300)
        {
            currentRound = 3;
            SpawnInterval = 0.75f;
        } else if (currentScore>=100)
        {
            currentRound = 2;
            SpawnInterval = 1.5f;
        } else
        {
            currentRound = 1;
            SpawnInterval = 3f;
        }

        if (Time.time>=NextSpawn)
        {
            SpawnEnemy();
            NextSpawn = Time.time+SpawnInterval;
        }
    }
    private void SpawnEnemy()
    {
        float randomY = UnityEngine.Random.Range(-Ybound, Ybound);
        int enemyRandomizer = UnityEngine.Random.Range(0,currentRound);
        Vector3 spawnPos = new Vector3(XBound, randomY);
        switch (enemyRandomizer)
        {
            case 0:
            Instantiate(ShooterPrefab, spawnPos, Quaternion.identity);
                break;
            case 1:
            Instantiate(AsteroidPrefab, spawnPos, Quaternion.identity);
            NextSpawn -= 1.5f;
                break;
            case 2:
            Instantiate(UFOSPrefab, spawnPos, Quaternion.identity);
                break;   
        }
    }
}
