using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float movementForce = 3f;
    [SerializeField] [Min(0f)] private float maxSpeed = 20f;
    [SerializeField] [Min(0f)] private float rotationSpeed = 10f;

    public Vector2 InputVector
    {
        get => _inputVector;
        set
        {
            if (value.magnitude > 0f) _lastNonZeroInput = value;
            _inputVector = value;
        }
    }

    private Vector2 _inputVector;
    private Vector2 _lastNonZeroInput;
    private Rigidbody2D _rb;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ProcessVelocity();
        ProcessRotation();
    }

    private void ProcessVelocity()
    {
        if (InputVector.magnitude == 0f) return;
        
        _rb.AddForce(InputVector.normalized * movementForce);
        
        var velocity = _rb.velocity;
        if (velocity.magnitude < maxSpeed) return;
        
        _rb.AddForce(-velocity.normalized * (velocity.magnitude - maxSpeed));
    }

    private void ProcessRotation()
    {
        if (_lastNonZeroInput.magnitude == 0f) return;

        var current = transform.eulerAngles.z;
        var goal = Vector2.SignedAngle(Vector2.up, _lastNonZeroInput);
        transform.rotation = Quaternion.Euler(Vector3.forward * Mathf.MoveTowardsAngle(current, goal, rotationSpeed * Time.deltaTime));
    }
}
