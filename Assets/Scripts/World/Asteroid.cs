using Components;
using UnityEngine;

namespace World
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(PickupDropper))]
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PickupDropper pickupDropper;
        [SerializeField] private PickupDropper gemPickupDropper;
        [SerializeField] private Health health;
        [SerializeField] [Range(0f, 1f)] private float chanceOfGemDrop;
        [SerializeField] [Min(0)] private int requiredShipTierToBreak;
    
        private static readonly int Hit = Animator.StringToHash("Hit");

        private void Awake()
        {
            animator.enabled = false;
            health.OnHit += OnHit;
            health.OnDeath += OnDeath;
        }

        private void Start()
        {
            if (GameManager.Instance.ShipTier < requiredShipTierToBreak)
            {
                health.Undying = true;
            }
        }

        private void OnHit(int amount)
        {
            animator.enabled = true;
            animator.SetTrigger(Hit);
            if (GameManager.Instance.ShipTier < requiredShipTierToBreak)
            {
                GameManager.Instance.TriggerNotification("Unable to break");
            }
        }

        private void OnDeath()
        {
            pickupDropper.Drop();
            if (chanceOfGemDrop > 0f && (Random.value < chanceOfGemDrop))
            {
                gemPickupDropper.Drop();
            }
            Destroy(gameObject);
        }
    }
}
