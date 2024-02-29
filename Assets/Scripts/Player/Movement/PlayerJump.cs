using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerJump : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerStatus _playerStatus;
    private Rigidbody2D _rigidBody2D;

    [Header("Jump configuration")]
    [SerializeField, Tooltip("Up force applied when jumping"), Min(0)]
    private float jumpForce;

    [SerializeField, Tooltip("Down force applied multiplier when jump button released"), Range(0, 1)]
    private float jumpCutMultiplier;

    [SerializeField,
     Tooltip(
         "Allow player to jump after falling from a platform (seconds) (high number can allow the player to jump infinitely!)"),
     Min(0)]
    private float coyoteTime;

    private float _lastTimeGrounded;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerStatus = GetComponent<PlayerStatus>();
        _rigidBody2D = GetComponentInChildren<Rigidbody2D>();
    }

    private void Start()
    {
        _playerController.OnPlayerJumpPressed += OnPlayerJumpPressed;
        _playerController.OnPlayerJumpReleased += OnPlayerJumpReleased;
    }

    private void FixedUpdate()
    {
        _lastTimeGrounded = _playerStatus.IsGrounded ? 0 : _lastTimeGrounded + Time.fixedDeltaTime;
    }

    private void OnPlayerJumpPressed()
    {
        var canJump = _playerStatus.IsGrounded || (_lastTimeGrounded <= coyoteTime && !_playerStatus.IsRising);

        if (canJump)
        {
            _rigidBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnPlayerJumpReleased()
    {
        if (!(_rigidBody2D.velocity.y > 0) || _playerStatus.IsGrounded) return;

        _rigidBody2D.AddForce(Vector2.down * _rigidBody2D.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
    }
}