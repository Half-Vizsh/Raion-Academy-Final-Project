using System;
using System.Collections;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Rendering;

public class Emy_Spawner : MonoBehaviour
{
    [SerializeField] GameObject ShooterPrefab;
    [SerializeField] GameObject AsteroidPrefab;
    [SerializeField] GameObject UFOSPrefab;
    [SerializeField] GameObject BossPrefab;
    [SerializeField] float SpawnInterval;
    [SerializeField] private float NextSpawn;
    [SerializeField] float Ybound;
    [SerializeField] float currentScore;
    [SerializeField] GameObject BossSpawnPos;
    private float XBound = 9f;
    private bool bossCanSpawn = true;
    private bool minionCanSpawn = true;
    public int currentRound;
    [Header("For Intro")]
    public BossUI bossUI;
    public GameObject gameOverUI;

    void Start()
    {
        //Please make an empty game object where you want to create the boss and add this tag
        if (BossSpawnPos == null) BossSpawnPos = GameObject.FindGameObjectWithTag("Boss Spawn Pos");
        bossUI.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        //Boss summon
        GameObject checkExistingEnemy = GameObject.FindWithTag("Enemy");
        currentScore = ScoreSystem.ScoreValue;
        if (currentScore >= 300&&bossCanSpawn)
        {
            minionCanSpawn = false;
            if (checkExistingEnemy==null){
            GameObject bossObj = Instantiate (BossPrefab,  BossSpawnPos.transform.position, UnityEngine.Quaternion.identity);
            Boss boss = bossObj.GetComponent<Boss>();
            boss.AssignUI(bossUI);
            bossUI.Show();
            boss.gameOverCanvas = gameOverUI;
            bossCanSpawn = false;
            } 
        }
        //Wave Manager
        if (currentScore >= 150&&bossCanSpawn)
        {
            currentRound = 3;
            SpawnInterval = 0.75f;
        } else if (currentScore>=50&&bossCanSpawn)
        {
            currentRound = 2;
            SpawnInterval = 1.1f;
        } else if (bossCanSpawn)
        {
            currentRound = 1;
            SpawnInterval = 3f;
        }
        ScoreSystem.RoundValue = currentRound;
        //Spawn Stuff
        if (Time.time>=NextSpawn&&minionCanSpawn)
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
