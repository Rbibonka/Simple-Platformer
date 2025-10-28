using System;
using UnityEngine.InputSystem;

public sealed class PlayerInputListenter : IDisposable
{
    private PlayerInput playerInput;

    public event Action<float> InputMoved;
    public event Action InputJumped;
    public event Action InputMoveEnded;

    public PlayerInputListenter()
    {
        playerInput = new PlayerInput();

        playerInput.Player.Move.performed += Move;
        playerInput.Player.Jump.performed += Jump;
        playerInput.Player.Move.canceled += EndMove;

        StartListen();
    }

    public void StopListen()
    {
        playerInput.Player.Disable();
    }

    public void StartListen()
    {
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

    private void EndMove(InputAction.CallbackContext callbackContext)
    {
        InputMoveEnded?.Invoke();
    }

    public void Dispose()
    {
        playerInput.Player.Move.performed -= Move;
        playerInput.Player.Jump.performed -= Jump;
        playerInput.Player.Move.canceled -= EndMove;

        playerInput.Player.Disable();
    }
}