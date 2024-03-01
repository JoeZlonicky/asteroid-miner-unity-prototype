using System.Collections.Generic;
using Components;
using ScriptableObjects;
using UnityEngine;

namespace UI
{
    public class InventoryPanel : MonoBehaviour
    {
        public Inventory connectedInventory;
        
        private ItemSlot[] _itemSlots;
        private Dictionary<ItemData, ItemSlot> _correspondingSlots;

        private void Awake()
        {
            _correspondingSlots = new Dictionary<ItemData, ItemSlot>();
            _itemSlots = GetComponentsInChildren<ItemSlot>();
            
            Debug.Assert(connectedInventory != null);
            connectedInventory.OnItemAdded += OnItemAddedToInventory;
            connectedInventory.OnItemRemoved += OnItemRemovedFromInventory;
        }

        private void OnItemAddedToInventory(ItemData itemData, int n, bool isNew)
        {
            var slot = isNew ? StartNewSlot(itemData) : _correspondingSlots[itemData];
            slot.SetItemCount(itemData, connectedInventory.GetItemCount(itemData));
        }

        private void OnItemRemovedFromInventory(ItemData itemData, int n, bool isLastOf)
        {
            var slot = _correspondingSlots[itemData];
            if (isLastOf)
            {
                slot.Clear();
                _correspondingSlots.Remove(itemData);
                return;
            }
            
            slot.SetItemCount(itemData, connectedInventory.GetItemCount(itemData));
        }

        private ItemSlot StartNewSlot(ItemData itemData)
        {
            foreach (var slot in _itemSlots)
            {
                if (slot.IsFilled()) continue;

                _correspondingSlots[itemData] = slot;
                return slot;
            }
            
            Debug.Assert(false);
            return null;
        }
    }
}
