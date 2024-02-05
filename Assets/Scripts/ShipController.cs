using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class ShipController : MonoBehaviour
{
    [field: SerializeField] private float ForwardForce { get; set; } = 3f;
    [field: SerializeField] private float MaxSpeed { get; set; } = 50f;
    
    [field: SerializeField] private float PassiveDrag { get; set; } = 0.1f;
    [field: SerializeField] private float ActiveDrag { get; set; } = 2f;
    [field: SerializeField] private float DragAcceleration { get; set; } = 50f;

    [field: SerializeField] private float TorqueForce { get; set; } = 1.5f;
    
    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _moveInput = new Vector2();
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
                ApplyForwardForce();
                break;
            case < 0f:
                ApplyActiveDrag();
                break;
            default:
                ApplyPassiveDrag();
                break;
        }
    }

    private void ProcessRotation()
    {
        float rotateInput = _moveInput.x;
        if (rotateInput != 0f)
        {
            ApplyTorque(-Mathf.Sign(rotateInput));
        }
    }

    private void ApplyForwardForce()
    {
        var direction = gameObject.transform.rotation * Vector2.up;
        _rb.AddForce(direction * ForwardForce);
        
        var velocity = _rb.velocity;
        if (velocity.magnitude > MaxSpeed)
        {
            Debug.Log("Max speed");
            _rb.AddForce(-velocity.normalized * (velocity.magnitude - MaxSpeed));
        }
    }

    private void ApplyActiveDrag()
    {
        _rb.drag = Mathf.MoveTowards(_rb.drag, ActiveDrag, DragAcceleration * Time.deltaTime);
    }

    private void ApplyPassiveDrag()
    {
        _rb.drag = Mathf.MoveTowards(_rb.drag, PassiveDrag, DragAcceleration * Time.deltaTime);
    }

    private void ApplyTorque(float sign)
    {
        _rb.AddTorque(sign * TorqueForce);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
}
