using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class ShipInputHandler : MonoBehaviour
    {
        [SerializeField] private ShipController ship;
        [SerializeField] private LaserBlaster blaster;
        
        private InputActions _inputActions;
        private InputAction _moveInput;
        private InputAction _fireInput;

        private void Awake()
        {
            _inputActions = new InputActions();
        }
        
        private void OnEnable()
        {
            _moveInput = _inputActions.Player.Move;
            _moveInput.Enable();
            _moveInput.performed += OnMoveInput;
            _moveInput.canceled += OnMoveInput;

            _fireInput = _inputActions.Player.Fire;
            _fireInput.Enable();
            _fireInput.performed += OnFireInput;
            _fireInput.canceled += OnFireInput;
        }

        private void OnDisable()
        {
            _moveInput.Disable();
            _fireInput.Disable();
        }
        
        private void OnMoveInput(InputAction.CallbackContext context)
        {
            ship.InputVector = context.ReadValue<Vector2>();
        }

        private void OnFireInput(InputAction.CallbackContext context)
        {
            blaster.Firing = context.ReadValue<float>() > 0f;
        }
    }
}
