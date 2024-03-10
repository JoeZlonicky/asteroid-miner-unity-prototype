using Components;
using ScriptableObjects;
using UnityEngine;

namespace UI
{
    public class CraftingUI : MonoBehaviour
    {
        [SerializeField] private ItemSlot[] ingredientSlots;
        [SerializeField] private ItemSlot[] productSlots;

        private RecipeData _recipeData;

        public void SetRecipe(RecipeData recipeData)
        {
            Debug.Assert(recipeData.ingredients.Length <= ingredientSlots.Length);
            Debug.Assert(recipeData.products.Length <= productSlots.Length);
        
            _recipeData = recipeData;
        
            for (var i = 0; i < ingredientSlots.Length; ++i)
            {
                ingredientSlots[i].gameObject.SetActive(i < _recipeData.ingredients.Length);
                if (i >= _recipeData.ingredients.Length) continue;
            
                var itemQuantity = _recipeData.ingredients[i];
                ingredientSlots[i].SetItemCount(itemQuantity.itemData, itemQuantity.quantity);
            }

            for (var i = 0; i < productSlots.Length; ++i)
            {
                productSlots[i].gameObject.SetActive(i < _recipeData.products.Length);
                if (i >= _recipeData.products.Length) continue;
            
                var itemQuantity = _recipeData.products[i];
                productSlots[i].SetItemCount(itemQuantity.itemData, itemQuantity.quantity);
            }
        }

        public void UpdateSlots(Inventory inventory)
        {
            Debug.Assert(_recipeData != null);
        }
    }
}
