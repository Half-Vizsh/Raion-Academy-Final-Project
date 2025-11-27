using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int enemyHealth;
    public int maxEnemyHP = 50;

    public GameObject Explosion;
    void Start()
    {
        enemyHealth = maxEnemyHP;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name + " (Tag: " + collision.gameObject.tag + ")");
        
        if(collision.gameObject.CompareTag("Player")){
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Player Bullet"))
        {
            Ply_Bullet bullet = collision.gameObject.GetComponent<Ply_Bullet>();
            if(bullet != null)
            {
                enemyHealth -= bullet.damage;
                Debug.Log("Damage = " + bullet.damage + ", Enemy HP = " + enemyHealth);
                
                if(enemyHealth <= 0)
                {
                    Debug.Log("Enemy destroyed! HP was: " + enemyHealth);
                    ExplodeDestruction();
                }
            }
        }
    }

    void ExplodeDestruction()
    {
        Instantiate(Explosion, transform.position,Quaternion.identity);

        Destroy(gameObject);
    }   
}
