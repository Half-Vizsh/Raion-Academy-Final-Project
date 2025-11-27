using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Refs")]
    public GameObject bossBulletPrefab;   // assign prefab yang pakai BossBullet.cs
    public Transform fireOrigin;          // titik spawn peluru (child transform)
    public Transform playerTransform;     // optional, bisa auto-find

    [Header("Timing")]
    public float patternInterval = 1.8f;  // jeda antar pola
    public float baseFireRate = 0.12f;    // dasar delay antar peluru di pola

    [Header("Stats")]
    public int health = 100;

    void Start()
    {
        if (fireOrigin == null) fireOrigin = transform;
        if (playerTransform == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) playerTransform = p.transform;
        }

        StartCoroutine(BehaviorLoop());
    }

    IEnumerator BehaviorLoop()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            int pattern = Random.Range(0, 4); // 0..3
            switch (pattern)
            {
                case 0: yield return StartCoroutine(PatternStraight(1.5f)); break;
                case 1: yield return StartCoroutine(PatternSpread(1.6f)); break;
                case 2: yield return StartCoroutine(PatternSpiral(2.2f)); break;
                case 3: yield return StartCoroutine(PatternAimedBurst(1.8f)); break;
            }

            yield return new WaitForSeconds(patternInterval);
        }
    }

    IEnumerator PatternStraight(float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            SpawnBullet(Vector2.left, 10f);
            t += baseFireRate;
            yield return new WaitForSeconds(baseFireRate);
        }
    }

    IEnumerator PatternSpread(float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            int count = 5;
            float spreadAngle = 40f;
            for (int i = 0; i < count; i++)
            {
                float a = Mathf.Lerp(-spreadAngle, spreadAngle, i / (float)(count - 1));
                Vector3 dir = Quaternion.Euler(0, 0, a) * Vector3.left;
                SpawnBullet(dir, 9f);
            }
            yield return new WaitForSeconds(0.5f);
            t += 0.5f;
        }
    }

    IEnumerator PatternSpiral(float duration)
    {
        float t = 0f;
        float angle = 0f;
        float step = 15f;
        while (t < duration)
        {
            Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.left;
            SpawnBullet(dir, 8f);
            angle += step;
            t += baseFireRate;
            yield return new WaitForSeconds(baseFireRate);
        }
    }

    IEnumerator PatternAimedBurst(float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            if (playerTransform != null)
            {
                Vector3 toPlayer = (playerTransform.position - fireOrigin.position).normalized;
                SpawnBullet(toPlayer, 12f);
                SpawnBullet(Quaternion.Euler(0, 0, 10f) * toPlayer, 12f);
                SpawnBullet(Quaternion.Euler(0, 0, -10f) * toPlayer, 12f);
            }
            else
            {
                SpawnBullet(Vector2.left, 12f);
            }
            yield return new WaitForSeconds(0.6f);
            t += 0.6f;
        }
    }

    void SpawnBullet(Vector3 dir, float speed)
    {
        if (bossBulletPrefab == null) return;
        Vector3 spawnPos = fireOrigin.position;
        GameObject b = Instantiate(bossBulletPrefab, spawnPos, Quaternion.identity);

        // set tag supaya Movement kamu deteksi "Enemy Bullet"
        b.tag = "Enemy Bullet";

        BossBullet bb = b.GetComponent<BossBullet>();
        if (bb != null)
        {
            bb.Init(dir, speed, 6f);
        }
    }

    // contoh method untuk menerima damage (panggil dari peluru player atau trigger)
    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0) Die();
    }

    void Die()
    {
        // efek / anim bisa kamu tambahkan di sini
        ScoreSystem.ScoreValue += 1000;
        Destroy(gameObject);
    }
}
