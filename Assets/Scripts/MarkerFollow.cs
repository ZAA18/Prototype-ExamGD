using UnityEngine;

public class MarkerFollow : MonoBehaviour
{
    public Transform player;

    [Header("Settings")]
    public float smoothSpeed = 10f;
    public float fixedHeight = 5f; // height where marker stays

    void Start()
    {
        // lock the starting height so you don't have to set it manually
        //fixedHeight = transform.position.y;
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = new Vector3(
            player.position.x,
            fixedHeight,
            player.position.z
        );

        // smooth follow (like camera damping)
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}
