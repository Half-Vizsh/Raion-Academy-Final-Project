using UnityEngine;

public class BossUIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created  //It'is located in the Boss itself
    private BossUI ui;
    public float maxHP = 100;
    public float currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void AssignUI(BossUI uiRef)
    {
        ui = uiRef;
    }

    public void healthChecker(int curHP, int maxHP)
    {
        ui.UpdateHealth(curHP, maxHP);
    }
}
