using System;
using UnityEngine; 

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float forwardForce = 3f;
    [SerializeField] [Min(0f)] private float maxSpeed = 20f;
    
    [SerializeField] [Min(0f)] private float passiveDrag = 0.1f;
    [SerializeField] [Min(0f)] private float activeDrag = 2f;
    [SerializeField] [Min(0f)] private float dragAcceleration = 2f;

    [SerializeField] [Min(0f)] private float torqueForce  = 1.5f;
    
    [NonSerialized] public Vector2 InputVector = new();
    
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
        float forwardsInput = InputVector.y;
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
        float rotateInput = InputVector.x;
        if (rotateInput != 0f)
        {
            ApplyTorque(-Mathf.Sign(rotateInput));
        }
    }

    private void ApplyForwardForce()
    {
        var direction = transform.rotation * Vector2.up;
        _rb.AddForce(direction * forwardForce);
        
        var velocity = _rb.velocity;
        if (!(velocity.magnitude > maxSpeed)) return;
        
        _rb.AddForce(-velocity.normalized * (velocity.magnitude - maxSpeed));
    }

    private void ApplyActiveDrag()
    {
        _rb.drag = Mathf.MoveTowards(_rb.drag, activeDrag, dragAcceleration * Time.deltaTime);
    }

    private void ApplyPassiveDrag()
    {
        _rb.drag = Mathf.MoveTowards(_rb.drag, passiveDrag, dragAcceleration * Time.deltaTime);
    }

    private void ApplyTorque(float sign)
    {
        _rb.AddTorque(sign * torqueForce);
    }
}
