using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    [RequireComponent(typeof(ToggleCanvasGroupVisibility))]
    public class InventoryInputHandler : MonoBehaviour
    {
        [SerializeField] private ToggleCanvasGroupVisibility visibilityToggle;

        private InputActions _inputActions;
        private InputAction _inventoryAction;

        private void Awake()
        {
            _inputActions = new InputActions();
        }

        private void OnEnable()
        {
            _inventoryAction = _inputActions.Player.Inventory;
            _inventoryAction.Enable();
            _inventoryAction.performed += OnInventoryInput;
        }

        private void OnDisable()
        {
            _inventoryAction.Disable();
        }

        private void OnInventoryInput(InputAction.CallbackContext context)
        {
            visibilityToggle.Toggle();
        }
    }
}
