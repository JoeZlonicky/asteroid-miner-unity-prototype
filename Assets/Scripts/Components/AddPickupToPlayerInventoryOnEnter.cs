using UnityEngine;
using World;

namespace Components
{
    public class AddPickupToPlayerInventoryOnEnter : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var pickup = other.gameObject.GetComponent<Pickup>();
            var itemData = pickup.Data;
            GameManager.Instance.PlayerInventory.AddItem(itemData);
            pickup.Collect();
            GameManager.Instance.TriggerNotification($"+1 {itemData.displayName}");
        }
    }
}
