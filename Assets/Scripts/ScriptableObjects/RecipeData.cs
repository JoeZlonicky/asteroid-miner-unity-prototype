using System;
using System.Linq;
using Components;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName="Recipe", menuName="ScriptableObjects/Recipe")]
    public class RecipeData : ScriptableObject
    {
        public ItemQuantity[] ingredients;
        public ItemQuantity[] products;
        public string customCraftNotificationMessage;

        public bool CanCraft(Inventory inventory)
        {
            return ingredients.All(itemQuantity => inventory.HasItem(itemQuantity.itemData, itemQuantity.quantity));
        }
    }
    
    [Serializable]
    public struct ItemQuantity
    {
        public ItemData itemData;
        public int quantity;
    }
}
