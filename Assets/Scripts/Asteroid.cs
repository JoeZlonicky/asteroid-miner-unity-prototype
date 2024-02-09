using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class Asteroid : MonoBehaviour
{
    private Animator _animator;
    private Health _health;
    private static readonly int Hit = Animator.StringToHash("Hit");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
        Destroy(gameObject);
    }
}
