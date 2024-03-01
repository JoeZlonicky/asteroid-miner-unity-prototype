using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float movementForce = 50f;
    [SerializeField] [Min(0f)] private float rotationSpeed = 500f;

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
        MoveInDirectionOfInput();
        RotateTowardsLastDirection();
    }

    private void MoveInDirectionOfInput()
    {
        if (InputVector.magnitude == 0f) return;

        _rb.AddForce(InputVector.normalized * movementForce);
    }

    private void RotateTowardsLastDirection()
    {
        if (_lastNonZeroInput.magnitude == 0f) return;

        var current = transform.eulerAngles.z;
        var goal = Vector2.SignedAngle(Vector2.up, _lastNonZeroInput);
        transform.rotation = Quaternion.Euler(Vector3.forward * Mathf.MoveTowardsAngle(current, goal, rotationSpeed * Time.deltaTime));
    }
}
