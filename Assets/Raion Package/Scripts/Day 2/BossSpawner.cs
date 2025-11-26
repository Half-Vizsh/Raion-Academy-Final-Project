using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public bool spawnOnStart = true;
    public float delay = 0f;

    private bool hasSpawned = false;

    void Start()
    {
        if (spawnOnStart) StartCoroutine(SpawnOnce());
    }

    IEnumerator SpawnOnce()
    {
        if (hasSpawned) yield break;
        hasSpawned = true;

        if (delay > 0f) yield return new WaitForSeconds(delay);

        if (bossPrefab == null)
        {
            Debug.LogWarning("BossSpawner: bossPrefab belum di-assign!");
            yield break;
        }

        Instantiate(bossPrefab, transform.position, Quaternion.identity);
    }

    // jika kamu mau spawn lewat event lain, panggil SpawnNow()
    public void SpawnNow()
    {
        if (!hasSpawned) StartCoroutine(SpawnOnce());
    }
}
