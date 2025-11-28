using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

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
    private float currentEnergy;
    [SerializeField] float SkillCD;
    [SerializeField] float skillCost;
    private float nextSkill;
    [SerializeField] Image SkillIcon; 
    
    [Header("Audio")]
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip skillSound;
    [SerializeField] float shootVolume = 0.5f;
    [SerializeField] float skillVolume = 0.7f;
    private AudioSource audioSource;
    
    void Start()
    {
        // Tambahkan AudioSource component secara otomatis jika belum ada
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    void Update()
    {
        // Shooting
        if (nextShoot <= 0)
        {
            if (Input.GetButton("Fire1"))
            {
                Instantiate(bulletPrefab, shootingPoint.position, quaternion.identity);
                
                // Play shooting sound
                if (shootSound != null)
                {
                    audioSource.PlayOneShot(shootSound, shootVolume);
                }
                
                nextShoot += shootInterval;
            }
        } 
        else
        {
            nextShoot -= Time.deltaTime;
        }
        
        // Skill
        if (currentEnergy >= skillCost && Time.time >= nextSkill)
        {
            SkillIcon.enabled = enabled;
            if (Input.GetButtonDown("Fire2"))
            {
                Instantiate(rocketPrefab, shootingPoint.position, quaternion.identity);
                
                // Play skill sound
                if (skillSound != null)
                {
                    audioSource.PlayOneShot(skillSound, skillVolume);
                }
                
                currentEnergy -= skillCost;
                nextSkill = Time.time + SkillCD;
            }
        } else SkillIcon.enabled = !enabled;
    }
    
    public void gainEnergy(float energy)
    {
        currentEnergy += energy;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
    }
}