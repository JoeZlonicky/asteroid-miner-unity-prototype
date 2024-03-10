using System;
using Components;
using ScriptableObjects;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class CraftingStation : MonoBehaviour
{
    [SerializeField] private InteractableTrigger interactableTrigger;
    [SerializeField] private CraftingUI craftingUI;
    [FormerlySerializedAs("recipe")] [SerializeField] private RecipeData recipeData;

    private Inventory _inventory;

    private void Awake()
    {
        interactableTrigger.OnInteract += TryCrafting;
    }
    
    private void Start()
    {
        _inventory = GameManager.Instance.PlayerInventory;
        craftingUI.SetRecipe(recipeData);
        craftingUI.UpdateSlots(_inventory);
        _inventory.OnItemAdded += OnItemAddedToInventory;
        _inventory.OnItemRemoved += OnItemRemovedFromInventory;
    }

    private void TryCrafting()
    {
        if (!recipeData.CanCraft(_inventory)) return;

        foreach (var itemQuantity in recipeData.ingredients)
        {
            _inventory.RemoveItem(itemQuantity.itemData, itemQuantity.quantity);
        }

        foreach (var itemQuantity in recipeData.products)
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
