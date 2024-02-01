using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class ShipController : MonoBehaviour
{
    [field: SerializeField] private float ForwardAcceleration { get; set; } = 700f;
    [field: SerializeField] private float MaxSpeed { get; set; } = 700f;
    [field: SerializeField] private float PassiveSpeedDeceleration { get; set; } = 10f;

    [field: SerializeField] private float RotationAcceleration { get; set; } = 2f;
    [field: SerializeField] private float MaxRotationSpeed { get; set; } = 2f;
    [field: SerializeField] private float PassiveRotationDeceleration { get; set; } = 2f;

    [field: SerializeField] private float SlowDeceleration { get; set; } = 300f;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private float _currentRotationSpeed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _moveInput = new Vector2();
        _currentRotationSpeed = 0f;
    }

    private void FixedUpdate()
    {
        ProcessVelocity();
        ProcessRotation();
    }

    private void ProcessVelocity()
    {
        float forwardsInput = _moveInput.y;
        switch (forwardsInput)
        {
            case > 0f:
                ApplyActiveForwardAcceleration();
                break;
            case < 0f:
                ApplyActiveSlowDeceleration();
                break;
            default:
                ApplyPassiveSlowDeceleration();
                break;
        }
    }

    private void ProcessRotation()
    {
        float rotateInput = _moveInput.x;
        switch (rotateInput)
        {
            case 0f:
                ApplyPassiveRotationDeceleration();
                break;
            default:
                ApplyActiveRotationAcceleration(-Mathf.Sign(rotateInput));
                break;
        }
        gameObject.transform.Rotate(Vector3.forward, _currentRotationSpeed * Time.deltaTime);
    }

    private void ApplyActiveForwardAcceleration()
    {
        var direction = gameObject.transform.rotation * Vector2.up;
        _rb.velocity = Vector2.MoveTowards(_rb.velocity, direction * MaxSpeed, ForwardAcceleration * Time.deltaTime);
    }

    private void ApplyActiveSlowDeceleration()
    {
        _rb.velocity = Vector2.MoveTowards(_rb.velocity, Vector2.zero, ForwardAcceleration * Time.deltaTime);
    }

    private void ApplyPassiveSlowDeceleration()
    {
        _rb.velocity = Vector2.MoveTowards(_rb.velocity, Vector2.zero, PassiveSpeedDeceleration * Time.deltaTime);
    }

    private void ApplyActiveRotationAcceleration(float sign)
    {
        _currentRotationSpeed = Mathf.MoveTowards(_currentRotationSpeed, sign * MaxRotationSpeed, RotationAcceleration);
    }
    
    private void ApplyPassiveRotationDeceleration()
    {
        _currentRotationSpeed =
            Mathf.MoveTowards(_currentRotationSpeed, 0f, PassiveRotationDeceleration * Time.deltaTime);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
}
