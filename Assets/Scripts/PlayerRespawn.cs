using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Lives")]
    public int lives = 3;

    [Header("Respawn")]
    public Transform spawnPoint;

    [Header("UI")]
    public GameObject gameOverPanel;

    private Rigidbody rb;
    private MeshRenderer mesh;

    [Header("Referencing scripts")]
    private PlayerMove movementScript;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip deathSound;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
        movementScript = GetComponent<PlayerMove>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Starting spawn point
        spawnPoint = null;

        gameOverPanel.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        // CHECKPOINT
        if (other.CompareTag("Checkpoint"))
        {
            spawnPoint = other.transform;

            Debug.Log("Checkpoint Reached!");
        }

        // LAVA
        if (other.CompareTag("Lava"))
        {
            Die();
        }
    }

    /* void Die()
     {
         lives--;

         Debug.Log("Lives Left: " + lives);

         // Hide player
         mesh.enabled = false;

         // Stop movement
         rb.linearVelocity = Vector3.zero;

         // Wait before respawn
         Invoke(nameof(Respawn), 1.5f);
     } */

    void Die()
    {
        lives--;

        Debug.Log("Lives Left: " + lives);

        // Play death sound
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Hide player
        mesh.enabled = false;

        // Stop movement FIRST
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Disable movement
        movementScript.enabled = false;

        // THEN make kinematic
        rb.isKinematic = true;

        Invoke(nameof(Respawn), 1.5f);
    }

    /*void Respawn()
    {
        // If no lives left
        if (lives <= 0)
        {
            GameOver();
            return;
        }

        // Show player again
        mesh.enabled = true;

        // Move player to checkpoint
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position + Vector3.up * 2f;
        }
        else
        {
            transform.position = Vector3.zero;
        }
    }
    */


    void Respawn()
    {
        if (lives <= 0)
        {
            GameOver();
            return;
        }

        // Turn physics back on FIRST
        rb.isKinematic = false;

        // Move player
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position + Vector3.up * 2f;
        }
        else
        {
            transform.position = Vector3.zero;
        }

        // Reset movement
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Show player
        mesh.enabled = true;

        // Enable movement again
        movementScript.enabled = true;
    }



    void GameOver()
    {
        gameOverPanel.SetActive(true);

        // Unlock mouse
        Cursor.lockState = CursorLockMode.None;

        // Show mouse
        Cursor.visible = true;

        gameObject.SetActive(false);
    }

    // RETRY BUTTON
    public void RetryGame()
    {
        SceneManager.LoadScene(1);
    }

    // MAIN MENU BUTTON
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
