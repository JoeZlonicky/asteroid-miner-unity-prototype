using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
{
    public delegate void DeathAction();
    public event DeathAction OnDeath;

    public delegate void HitAction(int amount);
    public event HitAction OnHit;
    
    [SerializeField] [Min(0)] private int maxHealth = 10;

    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public int DealDamage(int amount)
    {
        if (_currentHealth == 0 || amount == 0) return 0;
        Debug.Assert(amount > 0);

        var before = _currentHealth;
        _currentHealth = Mathf.Max(0, _currentHealth - amount);

        var change = before - _currentHealth;

        OnHit?.Invoke(change);
        if (_currentHealth == 0) OnDeath?.Invoke();
        
        return change;
    }
}
