
using UnityEngine;

public class Movements : MonoBehaviour
{
    [Tooltip("Movement speed in units/second")]
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverCanvas;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.sqrMagnitude > 1f)
            movement = movement.normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Isi kode disini
        if (other.gameObject.CompareTag("Enemy")||other.gameObject.CompareTag("Enemy Bullet"))
        {
            Time.timeScale = 0f; //Freezing time
            if (gameOverCanvas != null)
            {
                //Turn object on
                gameOverCanvas.SetActive(true);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
