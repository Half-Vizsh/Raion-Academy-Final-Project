using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    private float SinceLastSpawn;
    public float spawnBoundY;
    public float spawnPosX;

    void Update()
    {
        SinceLastSpawn += Time.deltaTime;
        if (SinceLastSpawn>=spawnInterval)
        {
            SpawnEnemy();
            SinceLastSpawn=0f;
        }
    }
    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.Log ("Missing Enemey Prefab!");
            return ;    
        }
        float spawnPosY = Random.Range(-spawnBoundY, spawnBoundY);
        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0f);
        Instantiate (enemyPrefab, spawnPosition, Quaternion.identity);
    }
}