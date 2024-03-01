using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Components
{
    public class PickupDropper : MonoBehaviour
    {
        [SerializeField] private ItemData itemData;
        [SerializeField] [Min(1)] private int nToDrop = 1;
        [SerializeField] [Min(0f)] private float dropRadius = 1f;
        [SerializeField] [Min(0f)] private float passiveDriftForce = 5f;

        private void Awake()
        {
            Debug.Assert(itemData != null);
        }

        public void Drop()
        {
            var pickupPrefab = itemData.pickupPrefab;
            for (var i = 0; i < nToDrop; ++i)
            {
                var randomVector = Random.insideUnitCircle;
                Vector3 offset = dropRadius * randomVector;
                var pickup = Instantiate(pickupPrefab, transform.position + offset, Quaternion.identity);
                pickup.GetComponent<Rigidbody2D>().AddForce(passiveDriftForce * randomVector.normalized);
            }
        }
    }
}
