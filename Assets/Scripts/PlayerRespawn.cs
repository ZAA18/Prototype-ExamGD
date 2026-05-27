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

        // Hide player
        mesh.enabled = false;

        // Stop ALL movement
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Disable movement script
        movementScript.enabled = false;

        // Optional: make rigidbody stop reacting
        rb.isKinematic = true;

        // Wait before respawn
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
        // If no lives left
        if (lives <= 0)
        {
            GameOver();
            return;
        }

        // Move player FIRST
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position + Vector3.up * 2f;
        }
        else
        {
            transform.position = Vector3.zero;
        }

        // Reset physics
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = false;

        // Show player again
        mesh.enabled = true;

        // Re-enable movement
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // MAIN MENU BUTTON
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
