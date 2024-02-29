using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRun : MonoBehaviour
{
    private PlayerController _playerController;
    private Rigidbody2D _rigidBody2D;

    [Header("Run configuration")]
    [SerializeField, Tooltip("Maximum movement speed"), Range(0, 20)]
    private float moveSpeed;

    [SerializeField, Tooltip("Acceleration rate"), Range(0, 3)]
    private float acceleration;

    [SerializeField, Tooltip("Deceleration rate"), Range(0, 3)]
    private float deceleration;

    [SerializeField, Tooltip("Velocity of acceleration and deceleration"), Range(0, 3)]
    private float velocityPower;

    [SerializeField, Tooltip("Amount of friction applied when horizontal input is zero"), Min(0)]
    private float frictionAmount;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _rigidBody2D = GetComponentInChildren<Rigidbody2D>();
    }

    private void Start()
    {
        _playerController.OnPlayerRun += HandlePlayerRun;
    }

    private void HandlePlayerRun(InputAction inputAction)
    {
        var normalizedHorizontalInputValue = inputAction.ReadValue<Vector2>().x.NormalizeFloat();

        if (normalizedHorizontalInputValue == 0)
        {
            ApplyFriction();
        }

        ApplyRun(normalizedHorizontalInputValue);
    }

    private void ApplyFriction()
    {
        var frictionForce = CalculateFrictionForce();
        _rigidBody2D.AddForce(frictionForce, ForceMode2D.Impulse);
    }

    private void ApplyRun(int normalizedHorizontalInputValue)
    {
        var runForce = CalculateRunForce(normalizedHorizontalInputValue);
        _rigidBody2D.AddForce(runForce);
    }

    private Vector2 CalculateFrictionForce()
    {
        var frictionForce = Mathf.Min(Mathf.Abs(_rigidBody2D.velocity.x), Mathf.Abs(frictionAmount));
        frictionForce *= Mathf.Sign(_rigidBody2D.velocity.x);

        return Vector2.right * -frictionForce;
    }

    private Vector2 CalculateRunForce(int normalizedHorizontalInputValue)
    {
        var targetSpeed = normalizedHorizontalInputValue * moveSpeed;

        var speedDiff = targetSpeed - _rigidBody2D.velocity.x;

        var isTargetSpeedNonZero = Mathf.Abs(targetSpeed) > 0.01f;
        var accelerationRate = isTargetSpeedNonZero ? acceleration : deceleration;

        var movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelerationRate, velocityPower) * Mathf.Sign(speedDiff);

        return Vector2.right * movement;
    }
}