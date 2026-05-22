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


/*using UnityEngine;

public class Attachplayer : MonoBehaviour
{
    public GameObject player;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = player.transform.lossyScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // Keep world position, rotation, and scale
            player.transform.SetParent(transform, true);

            // Force original world scale
            KeepWorldScale(player.transform, originalScale);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.SetParent(null, true);

            // Restore correct scale again
            KeepWorldScale(player.transform, originalScale);
        }
    }

    void KeepWorldScale(Transform obj, Vector3 worldScale)
    {
        Vector3 parentScale = Vector3.one;

        if (obj.parent != null)
        {
            parentScale = obj.parent.lossyScale;
        }

        obj.localScale = new Vector3(
            worldScale.x / parentScale.x,
            worldScale.y / parentScale.y,
            worldScale.z / parentScale.z
        );
    }
}
*/

/*using UnityEngine;

public class Attachplayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;

    private void OnTriggerEnter (Collider other)
    {
        if ( other.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.gameObject == player)

        {
            player.transform.parent = null;
        }
    }
}
*/

/*using UnityEngine;

public class Attachplayer : MonoBehaviour
{
    public GameObject player;

    private Vector3 originalScale;

    private void Start()
    {
        // Save player's original scale so it NEVER changes
        originalScale = player.transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.SetParent(transform, true);

            // lock scale so platform cannot distort player
            player.transform.localScale = originalScale;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.SetParent(null, true);

            // restore scale just in case
            player.transform.localScale = originalScale;
        }
    }
}
*/
