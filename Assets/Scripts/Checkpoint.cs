using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated = false;

    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();

        // Default color
        rend.material.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;

            // Change color when activated
            rend.material.color = Color.green;

            Debug.Log("Checkpoint Activated!");
        }
    }
}
