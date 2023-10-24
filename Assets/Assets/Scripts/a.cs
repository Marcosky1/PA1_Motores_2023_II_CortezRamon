using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputActions : InputActionAsset
{
    public PlayerInputActions()
    {
        gamePlay = this.AddActionMap("GamePlay");

        Fire = gamePlay.AddAction("Fire", type: InputActionType.Button);
        Fire.AddBinding("<Mouse>/leftButton");

        Movement = gamePlay.AddAction("Movement", type: InputActionType.Value);
        Movement.AddBinding("<Gamepad>/leftStick");

        Aim = gamePlay.AddAction("Aim", type: InputActionType.Value);
        Aim.AddBinding("<Mouse>/position");

        gamepad = this.AddActionMap("Gamepad");

    }

    public InputAction Fire { get; private set; }
    public InputAction Movement { get; private set; }
    public InputAction Aim { get; private set; }

}
