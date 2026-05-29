using UnityEngine;

public class CameraTEsting : MonoBehaviour
{
    public Transform target;

    [Header("Settings")]
    public Vector3 offset =
        new Vector3(0f, 8f, -10f);

    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null)
            return;

        // Desired position
        Vector3 desiredPosition =
            target.position + target.TransformDirection(offset);

        // Smooth movement
        transform.position =
            Vector3.Lerp(
                transform.position,
                desiredPosition,
                smoothSpeed * Time.deltaTime
            );

        // Look slightly above player
        Vector3 lookTarget =
            target.position + Vector3.up * 2f;

        transform.LookAt(lookTarget);
    }
}
