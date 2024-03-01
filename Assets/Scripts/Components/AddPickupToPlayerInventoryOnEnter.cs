using UnityEngine;

namespace Components
{
    public class AddPickupToPlayerInventoryOnEnter : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var pickup = other.gameObject.GetComponent<Pickup>();
            GameManager.Instance.PlayerInventory.AddItem(pickup.Data);
            pickup.Collect();
        }
    }
}
