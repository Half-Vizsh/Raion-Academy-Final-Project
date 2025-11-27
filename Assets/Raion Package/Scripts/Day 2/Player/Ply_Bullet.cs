using UnityEngine;

public class Ply_Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] Rigidbody2D bulRb;
    [SerializeField] float lifeTime;
    [SerializeField] float energyPerBullet;
    public int damage;    
    private GameObject playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulRb.linearVelocity = transform.right * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <=0) Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (playerScript == null) playerScript = GameObject.FindGameObjectWithTag("Player");
            playerScript.GetComponent<Ply_Shoot>().gainEnergy(energyPerBullet);
            if (other.GetComponent<Boss>()!=null)other.GetComponent<Boss>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
