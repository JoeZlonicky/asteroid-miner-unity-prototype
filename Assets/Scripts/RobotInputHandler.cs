using UnityEngine;
using UnityEngine.InputSystem;

public class RobotInputHandler : MonoBehaviour
{
    [SerializeField] private RobotController robot;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        robot.InputVector = context.ReadValue<Vector2>();
    }
}
