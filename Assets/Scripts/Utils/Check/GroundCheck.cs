using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GroundCheck : MonoBehaviour
{
    public List<string> groundTags;
    public UnityEvent<bool> groundCheckStatusChanged;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!groundTags.Contains(other.gameObject.tag)) return;

        groundCheckStatusChanged?.Invoke(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!groundTags.Contains(other.gameObject.tag)) return;

        groundCheckStatusChanged?.Invoke(false);
    }
}