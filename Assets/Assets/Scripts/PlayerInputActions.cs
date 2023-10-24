using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions playerInputActions;
    private InputAction fireAction;
    private InputAction movementAction;
    private InputAction aimAction;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        fireAction = playerInputActions.GamePlay.Fire;
        movementAction = playerInputActions.GamePlay.Movement;
        aimAction = playerInputActions.GamePlay.Aim;
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    public void OnFire()
    {
        if (fireAction.triggered)
        {
            Fire();
        }
    }

    public void OnMovement()
    {
        Vector2 input = movementAction.ReadValue<Vector2>();
        Move(input);
    }

    public void OnAim()
    {
        Vector2 input = aimAction.ReadValue<Vector2>();
        Aim(input);
    }

}



