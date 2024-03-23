using ScriptableObjects;
using UnityEngine;

namespace Crafting
{
    public class PlatingRepairStation : CraftingStation
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
            if (GameManager.Instance.PlayerHealth.IsFullHealth())
            {
                GameManager.Instance.TriggerNotification("Ship already at full plating");
                return;
            }
            if (!_recipeData.CanCraft(_inventory))
            {
                GameManager.Instance.TriggerNotification("Insufficient resources");
                return;
            }

            foreach (var itemQuantity in _recipeData.ingredients)
            {
                _inventory.RemoveItem(itemQuantity.itemData, itemQuantity.quantity);
            }

            GameManager.Instance.PlayerHealth.Heal(1);
            GameManager.Instance.TriggerNotification($"Restored 1 plating");
        }
    }
}