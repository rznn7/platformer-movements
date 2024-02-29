using UnityEngine;
using UnityEngine.Events;

public class FallCheck : MonoBehaviour
{
    [SerializeField, Tooltip("The Rigidbody2D on which we check if it is falling")]
    private Rigidbody2D rigidBody2D;

    public UnityEvent<bool> fallCheckStatusChanged;

    private bool _currentIsFalling;

    private void FixedUpdate()
    {
        var newIsFalling = rigidBody2D.velocity.y < -0.1f;

        if (newIsFalling == _currentIsFalling) return;

        fallCheckStatusChanged?.Invoke(newIsFalling);
        _currentIsFalling = newIsFalling;
    }
}