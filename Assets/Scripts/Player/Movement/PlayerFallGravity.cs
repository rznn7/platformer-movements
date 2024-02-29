using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallGravity : MonoBehaviour
{
    private PlayerStatus _playerStatus;
    private Rigidbody2D _rigidbody2D;

    [SerializeField, Tooltip("Gravity multiplier when falling (exponential)"), Min(0)]
    private float fallGravityMultiplier;

    [SerializeField, Tooltip("Maximum fall speed"), Min(0)]
    private float maxFallSpeed;

    private float _initialGravityScale;

    private void Awake()
    {
        _playerStatus = GetComponent<PlayerStatus>();
        _rigidbody2D = GetComponentInChildren<Rigidbody2D>();
    }

    private void Start()
    {
        _initialGravityScale = _rigidbody2D.gravityScale;
    }

    private void FixedUpdate()
    {
        if (_playerStatus.IsFalling)
        {
            MultipleGravity();
        }
        else
        {
            ResetGravity();
        }

        CapMaximumFallSpeed();
    }

    private void MultipleGravity()
    {
        _rigidbody2D.gravityScale *= fallGravityMultiplier;
    }

    private void ResetGravity()
    {
        _rigidbody2D.gravityScale = _initialGravityScale;
    }

    private void CapMaximumFallSpeed()
    {
        var currentVelocity = _rigidbody2D.velocity;
        _rigidbody2D.velocity = new Vector2(currentVelocity.x, Mathf.Max(currentVelocity.y, -maxFallSpeed));
    }
}