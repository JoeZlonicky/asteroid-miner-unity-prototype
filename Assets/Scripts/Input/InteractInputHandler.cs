using Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InteractInputHandler : MonoBehaviour
    {
        [SerializeField] private InteractableTrigger interactableTrigger;

        private InputActions _inputActions;
        private InputAction _interactAction;

        private void Awake()
        {
            _inputActions = new InputActions();
        }
        private void OnEnable()
        {
            _interactAction = _inputActions.Player.Interact;
            _interactAction.Enable();
            _interactAction.performed += OnInteractInput;
        }

        private void OnInteractInput(InputAction.CallbackContext context)
        {
            interactableTrigger.CheckForInteraction();
        }
    }
}