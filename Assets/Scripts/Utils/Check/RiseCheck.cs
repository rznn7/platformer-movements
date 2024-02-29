using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class RiseCheck : MonoBehaviour
{
    [SerializeField, Tooltip("The Rigidbody2D on which we check if it is rising")]
    private Rigidbody2D rigidBody2D;

    public UnityEvent<bool> riseCheckStatusChanged;

    private bool _currentIsRising;

    private void FixedUpdate()
    {
        var newIsRising = rigidBody2D.velocity.y > 0.1f;

        if (newIsRising == _currentIsRising) return;

        riseCheckStatusChanged?.Invoke(newIsRising);
        _currentIsRising = newIsRising;
    }
}