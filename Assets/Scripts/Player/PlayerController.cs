using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputAction _playerInputAction;
    private InputAction _movements;

    public event Action<InputAction> OnPlayerRun;
    public event Action OnPlayerJumpPressed;
    public event Action OnPlayerJumpReleased;

    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
    }

    private void OnEnable()
    {
        _movements = _playerInputAction.Player.Movement;
        _movements.Enable();

        _playerInputAction.Player.Jump.performed += OnJumpPerformed;
        _playerInputAction.Player.Jump.Enable();

        _playerInputAction.Player.Jump.canceled += OnJumpCanceled;
        _playerInputAction.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        _movements.Disable();
        _playerInputAction.Player.Jump.performed -= OnJumpPerformed;
        _playerInputAction.Player.Jump.canceled -= OnJumpCanceled;
        _playerInputAction.Player.Jump.Disable();
    }

    private void FixedUpdate()
    {
        OnPlayerRun?.Invoke(_movements);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnPlayerJumpPressed?.Invoke();
    }

    private void OnJumpCanceled(InputAction.CallbackContext obj)
    {
        OnPlayerJumpReleased?.Invoke();
    }
}