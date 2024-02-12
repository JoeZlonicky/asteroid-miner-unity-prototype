using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject explosionPoint;

    [SerializeField] [Min(0)] private int damage = 1;
    [SerializeField] [Min(0f)] private float maxLifetimeSeconds = 5f;

    private void Awake()
    {
        Destroy(gameObject, maxLifetimeSeconds);
        Debug.Assert(explosionPrefab != null);
        Debug.Assert(explosionPoint != null);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var hitBox = other.gameObject.GetComponent<HitBox>();
        if (hitBox != null && hitBox.health)
        {
            hitBox.health.DealDamage(damage);
        }
        
        Instantiate(explosionPrefab, explosionPoint.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
