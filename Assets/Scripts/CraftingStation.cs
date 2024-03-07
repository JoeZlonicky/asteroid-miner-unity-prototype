using System;
using Components;
using ScriptableObjects;
using UI;
using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    [SerializeField] private InteractableTrigger interactableTrigger;
    [SerializeField] private CraftingUI craftingUI;
    [SerializeField] private Recipe recipe;

    private Inventory _inventory;

    private void Awake()
    {
        interactableTrigger.OnInteract += TryCrafting;
    }
    
    private void Start()
    {
        _inventory = GameManager.Instance.PlayerInventory;
        craftingUI.SetRecipe(recipe);
        craftingUI.UpdateSlots(_inventory);
        _inventory.OnItemAdded += OnItemAddedToInventory;
        _inventory.OnItemRemoved += OnItemRemovedFromInventory;
    }

    private void TryCrafting()
    {
        if (!recipe.CanCraft(_inventory)) return;

        foreach (var itemQuantity in recipe.ingredients)
        {
            _inventory.RemoveItem(itemQuantity.itemData, itemQuantity.quantity);
        }

        foreach (var itemQuantity in recipe.products)
        {
            _inventory.AddItem(itemQuantity.itemData, itemQuantity.quantity);
        }
    }

    private void OnDestroy()
    {
        if (_inventory == null) return;
        _inventory.OnItemAdded -= OnItemAddedToInventory;
        _inventory.OnItemRemoved -= OnItemRemovedFromInventory;
    }
    
    private void OnItemAddedToInventory(ItemData itemData, int n, bool isNew)
    {
        craftingUI.UpdateSlots(_inventory);
    }

    private void OnItemRemovedFromInventory(ItemData itemData, int n, bool isLastOf)
    {
        craftingUI.UpdateSlots(_inventory);
    }
}
