using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class RobotInputHandler : MonoBehaviour
    {
        [SerializeField] private RobotController robot;

        private InputActions _inputActions;
        private InputAction _moveInput;

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
        }

        private void OnDisable()
        {
            _moveInput.Disable();
        }

        private void OnMoveInput(InputAction.CallbackContext context)
        {
            robot.InputVector = context.ReadValue<Vector2>();
        }
    }
}
