using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class ShipController : MonoBehaviour
{
    [SerializeField] private float forwardForce = 3f;
    [SerializeField] private float maxSpeed = 20f;
    
    [SerializeField] private float passiveDrag = 0.1f;
    [SerializeField] private float activeDrag = 2f;
    [SerializeField] private float dragAcceleration = 2f;

    [SerializeField] private float torqueForce  = 1.5f;
    
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

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
}
