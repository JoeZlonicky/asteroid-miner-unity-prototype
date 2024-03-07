using Components;
using ScriptableObjects;
using UnityEngine;

namespace UI
{
    public class CraftingUI : MonoBehaviour
    {
        [SerializeField] private ItemSlot[] ingredientSlots;
        [SerializeField] private ItemSlot[] productSlots;

        private Recipe _recipe;

        public void SetRecipe(Recipe recipe)
        {
            Debug.Assert(recipe.ingredients.Length <= ingredientSlots.Length);
            Debug.Assert(recipe.products.Length <= productSlots.Length);
        
            _recipe = recipe;
        
            for (var i = 0; i < ingredientSlots.Length; ++i)
            {
                ingredientSlots[i].gameObject.SetActive(i < _recipe.ingredients.Length);
                if (i >= _recipe.ingredients.Length) continue;
            
                var itemQuantity = _recipe.ingredients[i];
                ingredientSlots[i].SetItemCount(itemQuantity.itemData, itemQuantity.quantity);
            }

            for (var i = 0; i < productSlots.Length; ++i)
            {
                productSlots[i].gameObject.SetActive(i < _recipe.products.Length);
                if (i >= _recipe.products.Length) continue;
            
                var itemQuantity = _recipe.products[i];
                productSlots[i].SetItemCount(itemQuantity.itemData, itemQuantity.quantity);
            }
        }

        public void UpdateSlots(Inventory inventory)
        {
            Debug.Assert(_recipe != null);
        }
    }
}
