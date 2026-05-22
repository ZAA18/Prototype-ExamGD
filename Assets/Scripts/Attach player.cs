using UnityEngine;

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
