using System.Collections;
using UnityEditor.Callbacks;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class Emy_UfoShoots : MonoBehaviour
{
    [SerializeField] float Streakinterval;
    public float moveSpeed;
    public float fireRate = 2f;
    private float fireCountdown = 0f;
    private Vector3 lastDirection = Vector3.up;
    private int facing = 1;
    public GameObject enemyBulletPrefab;
    private Rigidbody2D rb;
    [Header("Sound")]
    [SerializeField] AudioClip ShootingSound;
    [SerializeField] float ShootingVolume = 0.7f;
     private AudioSource audioSource;
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime*moveSpeed);
        fireCountdown += Time.deltaTime;
        if (fireCountdown >= fireRate)
        {
            StartCoroutine("ShootingStreak");
            fireCountdown = 0;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float yRot = transform.eulerAngles.y;
        if (Mathf.Approximately(Mathf.DeltaAngle(yRot, 180f), 0f) || transform.localScale.x < 0f) facing = -1;
        else facing = 1;

        var sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            sr.flipX = (facing == 1);
        }
        //Audio
         // Tambahkan AudioSource component secara otomatis jika belum ada
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Shoot()
    {
        if (enemyBulletPrefab != null)
        {
            //Spawn Location
            Vector3 spawnPosition = transform.position;
            GameObject bullet = Instantiate(enemyBulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
             audioSource.PlayOneShot(ShootingSound, ShootingVolume);
            if (rb != null)
            {
                rb.linearVelocity = Vector3.left * 10f;
            }
            bullet.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator ShootingStreak()
    {
        Shoot();
        yield return new WaitForSeconds(Streakinterval);
         Shoot();
        yield return new WaitForSeconds(Streakinterval);
         Shoot();
        yield return new WaitForSeconds(Streakinterval);
    }
}