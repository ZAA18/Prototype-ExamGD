using UnityEngine;

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


