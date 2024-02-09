using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveInDirection : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float speed = 10f;
    
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.rotation * Vector2.right * speed;
    }
}
