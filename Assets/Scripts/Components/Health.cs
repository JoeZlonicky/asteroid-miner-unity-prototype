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

        private void Awake()
        {
            CurrentHealth = MaxHealth;
        }

        public int DealDamage(int amount)
        {
            if (amount == 0) return 0;
            Debug.Assert(amount > 0);

            var before = CurrentHealth;
            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);

            var change = before - CurrentHealth;

            OnHit?.Invoke(change);
            if (CurrentHealth == 0 && !Undying) OnDeath?.Invoke();
        
            return change;
        }
    }
}
