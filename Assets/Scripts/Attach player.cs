/*using UnityEngine;

public class Attachplayer : MonoBehaviour
{
    private Vector3 lastPosition;

    private Transform currentPlayer;

    void Start()
    {
        lastPosition = transform.position;
    }

    void LateUpdate()
    {
        // How much the platform moved this frame
        Vector3 platformMovement = transform.position - lastPosition;

        // Move player with platform
        if (currentPlayer != null)
        {
            currentPlayer.position += platformMovement;
        }

        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentPlayer = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentPlayer = null;
        }
    }
}
*/

using UnityEngine;

public class Attachplayer : MonoBehaviour
{
    private Vector3 lastPosition;

    private Transform currentRider;

    void Start()
    {
        lastPosition = transform.position;
    }

    void LateUpdate()
    {
        // PLATFORM MOVEMENT
        Vector3 movement =
            transform.position - lastPosition;

        // MOVE RIDER WITH PLATFORM
        if (currentRider != null)
        {
            currentRider.position += movement;
        }

        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // PLAYER OR AI
        if (
            other.CompareTag("Player") ||
            other.CompareTag("AI")
        )
        {
            currentRider = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (
            other.CompareTag("Player") ||
            other.CompareTag("AI")
        )
        {
            currentRider = null;
        }
    }
}


