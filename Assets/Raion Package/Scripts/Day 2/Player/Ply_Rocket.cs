using UnityEngine;

public class Ply_Rocket : MonoBehaviour
{
    [SerializeField] float rocketSpeed;
    [SerializeField] float lifeTime;
    public int damage;
    private int hitCount;
    private Rigidbody2D Rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.linearVelocity = transform.right * rocketSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <=0) Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.GetComponent<Boss>()!=null)collision.GetComponent<Boss>().TakeDamage(damage);
            if (collision.GetComponent<EnemyHealth>()!=null)collision.GetComponent<EnemyHealth>().enemyHealth -=damage;
            hitCount +=1;
            if (hitCount >=3) Destroy(gameObject);
        }
    }
}
