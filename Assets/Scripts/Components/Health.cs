using UnityEngine;

namespace Components
{
    public delegate void DeathAction();
    public delegate void HitAction(int amount);
    
    public class Health : MonoBehaviour
    {
        public event DeathAction OnDeath;
        public event HitAction OnHit;
        
        [field: SerializeField] public int MaxHealth { get; private set; } = 10;
        [field: SerializeField] public bool Undying { get; set; }
        private int CurrentHealth { get; set; }
        private bool IsDead { get; set; }

        private void Awake()
        {
            CurrentHealth = MaxHealth;
        }

        public void DealDamage(int amount)
        {
            if (amount == 0 || IsDead) return;
            Debug.Assert(amount > 0);

            var before = CurrentHealth;
            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
            
            OnHit?.Invoke(before - CurrentHealth);
            if (CurrentHealth != 0 || Undying) return;
            
            OnDeath?.Invoke();
            IsDead = true;
        }

        public void Heal(int amount)
        {
            if (amount == 0 || IsDead) return;
            Debug.Assert(amount > 0);

            CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
        }

        public void RaiseMaxHealth(int amount)
        {
            Debug.Assert(amount > 0);
            MaxHealth += amount;
        }

        public bool IsFullHealth()
        {
            return CurrentHealth == MaxHealth;
        }
    }
}
