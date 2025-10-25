using System;
using System.Diagnostics;
using UnityEngine.InputSystem;

public class PlayerInputListenter : IDisposable
{
    private PlayerInput playerInput;

    public event Action<float> InputMoved;
    public event Action InputJumped;

    public PlayerInputListenter()
    {
        playerInput = new PlayerInput();

        playerInput.Player.Move.performed += Move;
        playerInput.Player.Jump.performed += Jump;

        playerInput.Player.Enable();
    }

    private void Jump(InputAction.CallbackContext callbackContext)
    {
        InputJumped?.Invoke();
    }

    private void Move(InputAction.CallbackContext callbackContext)
    {
        InputMoved?.Invoke(callbackContext.action.ReadValue<float>());
    }

    public void Dispose()
    {
        playerInput.Player.Move.performed -= Move;
        playerInput.Player.Jump.performed -= Jump;

        playerInput.Player.Disable();
    }
}