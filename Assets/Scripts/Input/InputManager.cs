using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    SystemInput systemInput;

    private void Awake()
    {
        systemInput = new();
    }

    private void OnEnable()
    {
        systemInput.Player.Enable();
        systemInput.Player.Move.performed += OnMovePerformed;
        systemInput.Player.Move.canceled += OnMoveCanceled;
        systemInput.Player.Jump.performed += OnJumpPerformed;
        systemInput.Player.Pause.performed += OnPausePerformed;
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        InputEvents.Pause?.Invoke();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        InputEvents.Jump?.Invoke();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        InputEvents.Move?.Invoke(Vector2.zero);
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        InputEvents.Move?.Invoke(direction);
    }

    private void OnDisable()
    {
        systemInput.Player.Move.performed -= OnMovePerformed;
        systemInput.Player.Move.canceled -= OnMoveCanceled;
        systemInput.Player.Jump.performed -= OnJumpPerformed;
        systemInput.Player.Pause.performed -= OnPausePerformed;
        systemInput.Player.Disable();
    }
}
