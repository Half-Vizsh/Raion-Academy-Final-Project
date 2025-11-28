using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    //Its located in the UI
    public UnityEngine.UI.Image bossHealthBar;
    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);

    public void UpdateHealth(int health, int maxhealth)
    {
        bossHealthBar.fillAmount = health/maxhealth;
    }
}
