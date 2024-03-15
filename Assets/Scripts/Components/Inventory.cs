using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Components
{
    public delegate void ItemAddedAction(ItemData itemData, int n, bool isNew);
    public delegate void ItemRemovedAction(ItemData itemData, int n, bool isLastOf);
    
    public class Inventory : MonoBehaviour
    {
        public event ItemAddedAction OnItemAdded;
        public event ItemRemovedAction OnItemRemoved;
        
        private Dictionary<ItemData, int> _items;

        private void Awake()
        {
            _items = new Dictionary<ItemData, int>();
        }

        public void AddItem(ItemData itemData, int n = 1)
        {
            var isNew = !_items.ContainsKey(itemData);
            _items[itemData] = _items.GetValueOrDefault(itemData, 0) + n;
            OnItemAdded?.Invoke(itemData, n, isNew);
        }

        public bool HasItem(ItemData itemData, int min = 1)
        {
            return GetItemCount(itemData) >= min;
        }

        public int GetItemCount(ItemData itemData)
        {
            return _items.GetValueOrDefault(itemData, 0);
        }

        public void RemoveItem(ItemData itemData, int n = 1)
        {
            Debug.Assert(_items.ContainsKey(itemData));
            Debug.Assert(_items[itemData] >= n);

            var currentCount = _items[itemData];
            var isLastOf = currentCount <= n;
        
            if (isLastOf)
            {
                _items.Remove(itemData);
            }
            else
            {
                _items[itemData] -= n;
            }
        
            OnItemRemoved?.Invoke(itemData, n, isLastOf);
        }

        public Dictionary<ItemData, int>.KeyCollection Items()
        {
            return _items.Keys;
        }
    }
}