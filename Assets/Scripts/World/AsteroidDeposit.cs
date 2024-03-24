using Components;
using UnityEngine;

namespace World
{
    public class AsteroidDeposit : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PickupDropper pickupDropper;
        [SerializeField] private PickupDropper gemPickupDropper;
        [SerializeField] private Health health;
        [SerializeField] [Range(0f, 1f)] private float spawnChance = 0.5f;
        [SerializeField] [Range(0f, 1f)] private float gemSpawnChance = 0.5f;
        [SerializeField] [Min(0)] private int requiredShipTierToBreak;
    
        private static readonly int Hit = Animator.StringToHash("Hit");

        private bool doesDropGems = false;

        private void Awake()
        {
            if (Random.value < spawnChance)
            {
                gameObject.SetActive(false);
                return;
            }
            
            animator.enabled = false;
            if (gemPickupDropper && Random.value < gemSpawnChance) doesDropGems = true;
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
            if (doesDropGems)
            {
                gemPickupDropper.Drop();
            }
            Destroy(gameObject);
        }
    }
}
