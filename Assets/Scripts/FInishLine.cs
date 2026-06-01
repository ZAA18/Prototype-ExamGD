/*using UnityEngine;

public class FInishLine : MonoBehaviour
{
    public GameObject youWinPanel;

    private bool finished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (finished)
            return;

        // CHECK PLAYER TAG
        if (other.CompareTag("Player"))
        {
            finished = true;

            Debug.Log("PLAYER WON!");

            // Show win panel
            if (youWinPanel != null)
            {
                youWinPanel.SetActive(true);
            }

            // Stop game
            Time.timeScale = 0f;

            // ======================================
            // UNLOCK CURSOR
            // ======================================

            Cursor.lockState =
                CursorLockMode.None;

            Cursor.visible = true;
        }
    }
}
*/
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [Header("Panels")]
    public GameObject youWinPanel;
    public GameObject gameOverPanel;

    [Header("Tracker System")]
    public Tracker tracker;

    private bool finished = false;

    private void Start()
    {
        if (youWinPanel != null)
        {
            youWinPanel.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (finished)
            return;

        // PLAYER TOUCHES FINISH
        if (other.CompareTag("Player"))
        {
            finished = true;

            // ======================================
            // CHECK PLAYER POSITION
            // ======================================

            int playerPosition =
                tracker.GetPlayerPosition();

            Debug.Log(
                "PLAYER FINISHED IN POSITION: "
                + playerPosition
            );

            // ======================================
            // WIN
            // ======================================

            if (playerPosition == 1)
            {
                if (youWinPanel != null)
                {
                    youWinPanel.SetActive(true);
                }

                Debug.Log("YOU WIN!");
            }
            else
            {
                // ======================================
                // LOSE
                // ======================================

                if (gameOverPanel != null)
                {
                    gameOverPanel.SetActive(true);
                }

                Debug.Log("GAME OVER!");
            }

            // ======================================
            // STOP GAME
            // ======================================

            // Time.timeScale = 0f;
            Time.timeScale = 0.0001f;
            // ======================================
            // UNLOCK CURSOR
            // ======================================

            Cursor.lockState =
                CursorLockMode.None;

            Cursor.visible = true;
        }
    }
}