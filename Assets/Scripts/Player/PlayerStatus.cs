using System;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool IsGrounded { get; private set; }
    public bool IsFalling { get; private set; }
    public bool IsRising { get; private set; }
    public bool IsMovingOnX { get; private set; }

    public void UpdateGroundedStatus(bool newValue)
    {
        IsGrounded = newValue;
    }

    public void UpdateFallingStatus(bool newValue)
    {
        IsFalling = newValue;
    }

    public void UpdateRisingStatus(bool newValue)
    {
        IsRising = newValue;
    }

    public void UpdateMovingOnXStatus(bool newValue)
    {
        IsMovingOnX = newValue;
    }
}