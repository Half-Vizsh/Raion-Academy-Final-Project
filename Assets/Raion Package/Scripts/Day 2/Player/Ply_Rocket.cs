using UnityEngine;

public class Ply_Rocket : MonoBehaviour
{
    [SerializeField] float rocketSpeed;
    [SerializeField] float lifeTime;
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
        if (collision.tag == "Enemy")
        {
            hitCount +=1;
            if (hitCount >=3) Destroy(gameObject);
        }
    }
}
