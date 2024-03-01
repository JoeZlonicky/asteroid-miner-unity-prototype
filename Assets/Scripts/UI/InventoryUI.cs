using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    [RequireComponent(typeof(ToggleCanvasGroupVisibility))]
    public class InventoryUI : MonoBehaviour
    {
        private ToggleCanvasGroupVisibility _visibilityToggle;
    
        private void Awake()
        {
            _visibilityToggle = GetComponent<ToggleCanvasGroupVisibility>();
        }

        public void OnInventoryInput(InputAction.CallbackContext context)
        {
            _visibilityToggle.Toggle();
        }
    }
}
