using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveInDirection : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float speed = 10f;
    [SerializeField] private bool useRotation = true;
    [SerializeField] private Vector2 customDirection;
    
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Assert(_rb.isKinematic);
    }

    private void FixedUpdate()
    {
        if (useRotation)
        {
            _rb.velocity = transform.rotation * Vector2.right * speed;
            return;
        }

        _rb.velocity = customDirection * speed;
    }
    
    public void MoveInCustomDirection(Vector2 direction)
    {
        customDirection = direction.normalized;
        useRotation = false;
    }

    public void UpdateSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
