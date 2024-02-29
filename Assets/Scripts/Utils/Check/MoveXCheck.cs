using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class MoveXCheck : MonoBehaviour
{
    [SerializeField, Tooltip("The Rigidbody2D on which we check if it is moving on X axis")]
    private Rigidbody2D rigidBody2D;

    public UnityEvent<bool> moveXCheckStatusChanged;

    private bool _currentIsMovingOnX;

    private void FixedUpdate()
    {
        var newIsMovingOnX = Mathf.Abs(rigidBody2D.velocity.x) > 0.1f;

        if (newIsMovingOnX == _currentIsMovingOnX) return;

        moveXCheckStatusChanged?.Invoke(newIsMovingOnX);
        _currentIsMovingOnX = newIsMovingOnX;
    }
}
