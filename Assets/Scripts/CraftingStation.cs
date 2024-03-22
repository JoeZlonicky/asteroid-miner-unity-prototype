using Components;
using ScriptableObjects;
using UI;
using UnityEngine;

public abstract class CraftingStation : MonoBehaviour
{
    [SerializeField] protected InteractableTrigger interactableTrigger;
    [SerializeField] protected CraftingUI craftingUI;
    
    protected Inventory _inventory;
    protected RecipeData _recipeData;
    
    protected virtual void Awake()
    {
        interactableTrigger.OnInteract += TryCrafting;
    }
    
    protected virtual void Start()
    {
        _inventory = GameManager.Instance.PlayerInventory;
        _inventory.OnItemAdded += OnItemAddedToInventory;
        _inventory.OnItemRemoved += OnItemRemovedFromInventory;
    }
    
    protected virtual void OnDestroy()
    {
        if (_inventory == null) return;
        _inventory.OnItemAdded -= OnItemAddedToInventory;
        _inventory.OnItemRemoved -= OnItemRemovedFromInventory;
    }
    
    protected abstract void TryCrafting();
    
    private void OnItemAddedToInventory(ItemData itemData, int n, bool isNew)
    {
        craftingUI.UpdateSlots(_inventory);
    }

    private void OnItemRemovedFromInventory(ItemData itemData, int n, bool isLastOf)
    {
        craftingUI.UpdateSlots(_inventory);
    }
}