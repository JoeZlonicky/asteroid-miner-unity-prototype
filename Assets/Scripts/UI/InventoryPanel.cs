using System.Collections.Generic;
using System.Linq;
using Components;
using ScriptableObjects;
using UnityEngine;

namespace UI
{
    public class InventoryPanel : MonoBehaviour
    {
        private Inventory _connectedInventory;
        private ItemSlot[] _itemSlots;
        private Dictionary<ItemData, ItemSlot> _correspondingSlots;

        private void Awake()
        {
            _correspondingSlots = new Dictionary<ItemData, ItemSlot>();
            _itemSlots = GetComponentsInChildren<ItemSlot>();
            foreach (var itemSlot in _itemSlots)
            {
                itemSlot.Clear();
            }
        }

        protected void ConnectInventory(Inventory inventory)
        {
            if (_connectedInventory != null)
            {
                DisconnectInventory();
            }
            
            _connectedInventory = inventory;
            foreach (var itemData in inventory.Items())
            {
                OnItemAddedToInventory(itemData, inventory.GetItemCount(itemData), true);
            }
            _connectedInventory.OnItemAdded += OnItemAddedToInventory;
            _connectedInventory.OnItemRemoved += OnItemRemovedFromInventory;
        }

        protected void DisconnectInventory()
        {
            _connectedInventory.OnItemAdded -= OnItemAddedToInventory;
            _connectedInventory.OnItemRemoved -= OnItemRemovedFromInventory;
            _connectedInventory = null;
        }

        private void OnItemAddedToInventory(ItemData itemData, int n, bool isNew)
        {
            var slot = isNew ? StartNewSlot(itemData) : _correspondingSlots[itemData];
            slot.SetItemCount(itemData, _connectedInventory.GetItemCount(itemData));
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
            
            slot.SetItemCount(itemData, _connectedInventory.GetItemCount(itemData));
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
