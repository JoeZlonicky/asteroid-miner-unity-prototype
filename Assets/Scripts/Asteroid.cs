using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PickupDropper))]
public class Asteroid : MonoBehaviour
{
    private Animator _animator;
    private PickupDropper _pickupDropper;
    private Health _health;
    
    private static readonly int Hit = Animator.StringToHash("Hit");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _pickupDropper = GetComponent<PickupDropper>();
        _health = GetComponent<Health>();
        _health.OnHit += OnHit;
        _health.OnDeath += OnDeath;
    }

    private void OnHit(int amount)
    {
        _animator.SetTrigger(Hit);
    }

    private void OnDeath()
    {
        _pickupDropper.Drop();
        Destroy(gameObject);
    }
}
