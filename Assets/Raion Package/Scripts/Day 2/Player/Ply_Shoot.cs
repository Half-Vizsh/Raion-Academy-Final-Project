using Unity.Mathematics;
using UnityEngine;

public class Ply_Shoot : MonoBehaviour
{
    [Header("Shooting Mechanism")]
    [SerializeField] float shootInterval;
    private float nextShoot; 
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootingPoint;
    [Header("Skill Mechanism")]
    [SerializeField] float maxEnergy;
    [SerializeField] GameObject rocketPrefab;
    private float currentEnergy; // hide this
    [SerializeField] float SkillCD;
    [SerializeField] float skillCost; // and this
    private float nextSkill;
    void Update()
    {
        //Shooting
        if (nextShoot<=0)
        {
            if (Input.GetButton("Fire1"))
            {
            Instantiate(bulletPrefab, shootingPoint.position, quaternion.identity);
            nextShoot+=shootInterval;
            }
        } else
        {
            nextShoot-=Time.deltaTime;
        }
        //Skill
        if (currentEnergy>=20&&Time.time>=nextSkill)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Instantiate(rocketPrefab, shootingPoint.position,quaternion.identity);
                currentEnergy -= skillCost;
                nextSkill = Time.time + SkillCD;
            }
        }
    }
    public void gainEnergy(float energy)
    {
        currentEnergy+=energy;
    }
}
