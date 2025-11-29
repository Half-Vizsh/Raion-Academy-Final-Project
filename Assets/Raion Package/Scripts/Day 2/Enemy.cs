using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float fireRate = 2f;
    private float fireCountdown = 0f;
    private Vector3 lastDirection = Vector3.up;
    private int facing = 1;
    public GameObject enemyBulletPrefab;
    private Rigidbody2D rb;
    [SerializeField] AudioClip SfxShoot;
    [SerializeField] float ShootVolume = 0.7f;
    [SerializeField] Animator enemyAnim;
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime*moveSpeed);
        fireCountdown += Time.deltaTime;
        if (fireCountdown >= fireRate)
        {
            Shoot();
            fireCountdown = 0;
        }
    }

    void Start()
    {
        enemyAnim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        float yRot = transform.eulerAngles.y;
        if (Mathf.Approximately(Mathf.DeltaAngle(yRot, 180f), 0f) || transform.localScale.x < 0f) facing = -1;
        else facing = 1;

        var sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            sr.flipX = (facing == 1);
        }
    }

    void Shoot()
    {
        if (enemyBulletPrefab != null)
        {
            if(SfxShoot != null)
            {
                AudioSource.PlayClipAtPoint(SfxShoot,transform.position,ShootVolume);
            }
            //Spawn Location
            Vector3 spawnPosition = transform.position;
            GameObject bullet = Instantiate(enemyBulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

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
}