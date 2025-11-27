using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float damage;
    private Vector3 _direction = Vector3.left;
    private float _speed = 8f;
    private float _lifetime = 5f;

    // dipanggil setelah Instantiate untuk set arah/kecepatan
    public void Init(Vector3 dir, float speed, float lifetime = 5f)
    {
        _direction = dir.normalized;
        _speed = speed;
        _lifetime = lifetime;
    }

    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0f) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // kalau kena Player -> hancurkan peluru (player menangani kematian sendiri di Movement)
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            return;
        }

        // Jika mengenai environment (mis. Ground/Obstacle), hancurkan juga
        if (other.CompareTag("Obstacle") || other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
