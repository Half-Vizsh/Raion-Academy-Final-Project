using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Ply_Health : MonoBehaviour
{
    [SerializeField] float Maxhp;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float invincibleDuration;
    private float currenthp;
    private bool isInvincible;

    void Start()
    {
        currenthp = Maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy Bullet")
        {
            takingDamage(collision.GetComponent<BossBullet>().damage);
        }
    }
    public void takingDamage(float amount)
    {
        if (isInvincible) return;

        currenthp -= amount;
        StartCoroutine(PlayerDamageEffect());
        if (currenthp<=0)
        {
             if (gameOverCanvas != null)
            {
                //Turn object on
                gameOverCanvas.SetActive(true);
            }
            ScoreSystem.ScoreValue = 0;
            Destroy(gameObject);
        }
    }
    IEnumerator PlayerDamageEffect()
    {
        isInvincible = true;
        //Bikin sprite kelap kelip
        if (sprite != null)
        {
            for (int i = 0; i < 4; i++)
            {
                sprite.enabled = false;
                yield return new WaitForSeconds(0.1f);
                sprite.enabled = true;
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            yield return new WaitForSeconds(invincibleDuration);
        }

        // small buffer
        yield return new WaitForSeconds(0.3f);
        isInvincible = false;
    }
}
