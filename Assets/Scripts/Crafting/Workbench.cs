using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Crafting
{
    public class Workbench : CraftingStation
    {
        [SerializeField] private RecipeData defaultRecipe;
        [SerializeField] private TierRecipe[] tierRecipes;
    
        private Dictionary<int, RecipeData> _tierToRecipe;
    
        protected override void Awake()
        {
            base.Awake();
            _tierToRecipe = new Dictionary<int, RecipeData>
            {
                [0] = defaultRecipe
            };
            foreach (var tierRecipe in tierRecipes)
            {
                _tierToRecipe[tierRecipe.tier] = tierRecipe.recipeData;
            }
        }
        protected override void Start()
        {
            base.Start();
            UpdateRecipeFromTier(GameManager.Instance.ShipTier);
            GameManager.Instance.OnShipTierUp += OnShipTierUp;
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.OnShipTierUp -= OnShipTierUp;
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

            GameManager.Instance.UpgradeShipTier();
            GameManager.Instance.TriggerNotification("Upgraded ship");
        }

        private void OnShipTierUp(int newTier)
        {
            UpdateRecipeFromTier(newTier);
        }

        private void UpdateRecipeFromTier(int tier)
        {
            if (_tierToRecipe.TryGetValue(tier, out var value))
            {
                _recipeData = value;
                craftingUI.SetRecipe(_recipeData);
                craftingUI.UpdateSlots(_inventory);
                return;
            }
            interactableTrigger.gameObject.SetActive(false);
            craftingUI.gameObject.SetActive(false);
        }

        [Serializable]
        private class TierRecipe
        {
            public int tier;
            public RecipeData recipeData;
        }
    }
}