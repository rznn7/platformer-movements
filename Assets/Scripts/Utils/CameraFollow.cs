using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float yOffset;
    public float xOffset;

    private void LateUpdate()
    {
        var targetPosition = target.position;
        var cameraPosition = new Vector3(targetPosition.x + xOffset, targetPosition.y + yOffset, -1);

        transform.position = cameraPosition;
    }
}