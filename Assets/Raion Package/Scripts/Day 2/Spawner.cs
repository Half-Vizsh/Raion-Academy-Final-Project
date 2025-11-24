using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject AsteroidsPrefab;
    public float SpawnInterval = 4f;
    public float MediumSpawnInterval = 3f;
    public float HardSpawnInterval = 1.5f;
    private const float HardDifficultTime = 60f;
    private const float MediumDifficultTime = 30f;
    private bool HardApplied = false;
    private bool MediumApplied = false;
    private float SinceLastSpawn; // sebagai counter untuk spawner
    private float TimeDifficult;
    public float spawnBoundaryY;
    public float spawnBoundaryX;

    void Update()
    {
        TimeDifficult += Time.deltaTime;
        SinceLastSpawn += Time.deltaTime;
        UpdateSpawnInterval();
        TrySpawn();
    }
    void UpdateSpawnInterval()
    {
        if(TimeDifficult >= HardDifficultTime && !HardApplied)
        {
            SpawnInterval = HardSpawnInterval;
            HardApplied = true;
        }
        else if(TimeDifficult >= MediumDifficultTime && !MediumApplied)
        {
            SpawnInterval = MediumSpawnInterval;
            MediumApplied = true;
        }
    }

    void TrySpawn()
    {
        if(SinceLastSpawn < SpawnInterval) return;

        if(TimeDifficult >= HardDifficultTime)
        {
            SpawnEnemy();
            SpawnAsteroids();
        }
        else if(TimeDifficult >= MediumDifficultTime)
        {
            SpawnEnemy();
            SpawnAsteroids();
        }
        else
        {
            SpawnEnemy();
        }

        SinceLastSpawn = 0f;
    }
    void SpawnEnemy()
    {
        if(EnemyPrefab == null)
        {
            Debug.Log("Prefab Enemy null");
            return;
        }
        float RandomY = Random.Range(-spawnBoundaryY,spawnBoundaryY);
        Vector3 spawnPosition = new Vector3(spawnBoundaryX,RandomY);
        // untuk melakukan spawning dapat menggunakan Instantiate() method,
        // Instantiation memiliki parameter game object, vector3, dan Quaternion
        GameObject enemy = Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity);
    }

    void SpawnAsteroids()
    {
        if(AsteroidsPrefab == null)
        {
            Debug.Log("Prefab Asteroid null");
            return;
        }
        float RandomY = Random.Range(-spawnBoundaryY,spawnBoundaryY);
        Vector3 spawnPosition = new Vector3(spawnBoundaryX,RandomY,0f);
        // untuk melakukan spawning dapat menggunakan Instantiate() method,
        // Instantiation memiliki parameter game object, vector3, dan Quaternion
        GameObject asteroids = Instantiate(AsteroidsPrefab, spawnPosition, Quaternion.identity);
    }
}