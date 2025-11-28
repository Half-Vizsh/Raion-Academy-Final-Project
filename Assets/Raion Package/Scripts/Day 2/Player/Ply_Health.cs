using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Ply_Health : MonoBehaviour
{
    [SerializeField] float Maxhp;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float invincibleDuration;
    private float currenthp;
    private bool isInvincible;
    public UnityEngine.UI.Image healthBar;

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
        if (collision.gameObject.tag == "Enemy")
        {
            takingDamage(5f);
        }
    }
    public void takingDamage(float amount)
    {
        if (isInvincible) return;
        currenthp -= amount;
        healthBar.fillAmount = currenthp/Maxhp;
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
