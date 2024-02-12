using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemQuantityPair> defaultItems;
    private Dictionary<ItemData, int> _items;

    private void Awake()
    {
        _items = new Dictionary<ItemData, int>();
        foreach (var pair in defaultItems.Where(pair => pair.item != null))
        {
            AddItem(pair.item, pair.n);
        }
    }

    public void AddItem(ItemData item, int n = 1)
    {
        _items[item] = _items.GetValueOrDefault(item, 1) + 1;
    }

    [Serializable]
    private class ItemQuantityPair
    {
        public ItemData item;
        [Min(1)] public int n;
    }
}