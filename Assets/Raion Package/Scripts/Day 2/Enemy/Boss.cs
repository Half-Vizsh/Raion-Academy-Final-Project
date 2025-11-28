using System.Collections;
using NUnit.Framework;
using UnityEditor.Build.Content;
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
    [Header("FX")]
    [SerializeField] AudioClip ShootingSound;
    [SerializeField] float ShootingVolume = 0.7f;
     private AudioSource audioSource;

    [Header("Stats")]
    [SerializeField] float MaxHealth;
    public float currentHealth = 100;
    [Header("Intro")]
    [SerializeField] float IntroSpeed; //Gerak pas intro
    public bool isInvincible = true;
    private BossUI ui;
    [Header("Outro")]
    [SerializeField] AudioClip ExplosionSound;
    [SerializeField] float ExplosionVolume = 0.7f;
    public GameObject Explosion;
    public GameObject gameOver;
    public GameObject gameOverCanvas;

    void Start()
    {
        currentHealth = MaxHealth;
        if (fireOrigin == null) fireOrigin = transform;
        if (playerTransform == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) playerTransform = p.transform;
        }
        //Audio
         // Tambahkan AudioSource component secara otomatis jika belum ada
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        StartCoroutine(BehaviorLoop());
        //Game Over
        gameOver =  GameObject.Find("GameOver");
        if (gameOver != null) Debug.Log ("Got it");
    }
    void Update()
    {
        //Small Intro
        if (gameObject.transform.position.x > 6)
        {
            transform.Translate(Vector3.left * Time.deltaTime*IntroSpeed);
            isInvincible = true;
        }
        else
        {
            isInvincible = false;
        }
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
            audioSource.PlayOneShot(ShootingSound, ShootingVolume);
            SpawnBullet(Vector2.left, 10f);
            t += baseFireRate;
            yield return new WaitForSeconds(baseFireRate);
        }
    }

    IEnumerator PatternSpread(float duration)
    {
        float t = 0f;
        audioSource.PlayOneShot(ShootingSound, ShootingVolume);
        while (t < duration)
        {
            audioSource.PlayOneShot(ShootingSound, ShootingVolume);
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
            audioSource.PlayOneShot(ShootingSound, ShootingVolume);
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
            audioSource.PlayOneShot(ShootingSound, ShootingVolume);
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
    public void AssignUI(BossUI uiRef)
    {
        ui = uiRef;
    }

    // contoh method untuk menerima damage (panggil dari peluru player atau trigger)
    public void TakeDamage(int dmg)
    {
        if (isInvincible) return;
        currentHealth -= dmg;
        if (currentHealth <= 0) Die();
        // if (ui != null) ui.UpdateHealth(currentHealth, MaxHealth);
        if (ui != null) ui.bossHealthBar.fillAmount = currentHealth/MaxHealth;
    }
    public void BossDefeated()
    {
        if (gameOverCanvas != null){
            gameOverCanvas.SetActive(true);
            gameOverCanvas.transform.Find("Restart").gameObject.SetActive(false);
        }
    }    
    void Die()
    {
        BossDefeated();
        if (gameOver!= null)
        {
            gameOver.SetActive(true);
            gameObject.transform.Find("Restart").gameObject.SetActive(false);
            Time.timeScale = 0f;
        }
        if (Explosion != null)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }

        if (ExplosionSound != null)
        {
            AudioSource.PlayClipAtPoint(ExplosionSound, transform.position, ExplosionVolume);
        }
        ScoreSystem.ScoreValue += 1000;
        Destroy(gameObject);
    }
}
