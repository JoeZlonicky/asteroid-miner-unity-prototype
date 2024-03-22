using ScriptableObjects;
using UnityEngine;

namespace Crafting
{
    public class MaterialForge : CraftingStation
    {
        [SerializeField] private RecipeData recipeData;
    
        protected override void Start()
        {
            base.Start();
            _recipeData = recipeData;
            craftingUI.SetRecipe(_recipeData);
            craftingUI.UpdateSlots(_inventory);
        }

        protected override void TryCrafting()
        {
            if (!_recipeData.CanCraft(_inventory))
            {
                GameManager.Instance.TriggerNotification("Unable to craft");
                return;
            }

            foreach (var itemQuantity in _recipeData.ingredients)
            {
                _inventory.RemoveItem(itemQuantity.itemData, itemQuantity.quantity);
            }

            foreach (var itemQuantity in _recipeData.products)
            {
                _inventory.AddItem(itemQuantity.itemData, itemQuantity.quantity);
            }
        
            foreach (var itemQuantity in _recipeData.products)
            {
                GameManager.Instance.TriggerNotification($"Crafted {itemQuantity.quantity} {itemQuantity.itemData.displayName}");
            }
        }
    }
}
