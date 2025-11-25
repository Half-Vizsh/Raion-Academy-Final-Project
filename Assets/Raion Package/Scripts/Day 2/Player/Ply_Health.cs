using Unity.Mathematics;
using UnityEngine;

public class Ply_Health : MonoBehaviour
{
    [SerializeField] float Maxhp;
    private float currenthp;

    void Start()
    {
        currenthp = Maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takingDamage(int amount)
    {
        currenthp -= amount;
        if (currenthp<=0)
        {
            Destroy(gameObject);
        }
    }
}
