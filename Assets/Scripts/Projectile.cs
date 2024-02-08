using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject explosionPoint;
    
    [SerializeField] [Min(0f)] private float maxLifetimeSeconds = 5f;
    [SerializeField] [Min(0f)] private float speed = 10f;

    private Rigidbody2D _rb;
    private float _lifetimeSeconds;

    private void Awake()
    {
        Debug.Assert(explosionPrefab != null);
        Debug.Assert(explosionPoint != null);
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = transform.rotation * Vector2.right * speed;
    }

    private void Update()
    {
        _lifetimeSeconds += Time.deltaTime;
        if (_lifetimeSeconds > maxLifetimeSeconds)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(explosionPrefab, explosionPoint.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
