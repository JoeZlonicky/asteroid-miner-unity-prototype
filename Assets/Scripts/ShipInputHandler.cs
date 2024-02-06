using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipInputHandler : MonoBehaviour
{
    [SerializeField] private ShipController ship;
    [SerializeField] private LaserBlaster blaster;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        ship.InputVector = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        blaster.Firing = context.ReadValue<float>() > 0f;
    }
}
