using Components;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PickupDropper))]
public class Asteroid : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PickupDropper pickupDropper;
    [SerializeField] private Health health;
    
    private static readonly int Hit = Animator.StringToHash("Hit");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        
        pickupDropper = GetComponent<PickupDropper>();
        health = GetComponent<Health>();
        health.OnHit += OnHit;
        health.OnDeath += OnDeath;
    }

    private void OnHit(int amount)
    {
        animator.enabled = true;
        animator.SetTrigger(Hit);
    }

    private void OnDeath()
    {
        pickupDropper.Drop();
        Destroy(gameObject);
    }
}
