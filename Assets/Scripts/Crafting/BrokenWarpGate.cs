﻿using ScriptableObjects;
using UnityEngine;

namespace Crafting
{
    public class BrokenWarpGate : CraftingStation
    {
        [SerializeField] private RecipeData recipeData;
        [SerializeField] private GameObject repairedPrefab;
    
        protected override void Start()
        {
            if (GameManager.Instance.IsWarpGateRepaired)
            {
                Repair();
                return;
            }
            
            base.Start();
            _recipeData = recipeData;
            craftingUI.SetRecipe(_recipeData);
            craftingUI.UpdateSlots(_inventory);
        }

        protected override void TryCrafting()
        {
            if (!_recipeData.CanCraft(_inventory))
            {
                GameManager.Instance.TriggerNotification("Insufficient resources");
                return;
            }

            foreach (var itemQuantity in _recipeData.ingredients)
            {
                _inventory.RemoveItem(itemQuantity.itemData, itemQuantity.quantity);
            }

            Repair();
            
            GameManager.Instance.SetWarpGateRepaired();
            GameManager.Instance.TriggerNotification($"Repaired warp gate");
        }

        private void Repair()
        {
            var t = transform;
            Instantiate(repairedPrefab, t.position, t.rotation);
            Destroy(gameObject);
        }
    }
}