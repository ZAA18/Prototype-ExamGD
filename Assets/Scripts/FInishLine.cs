using UnityEngine;

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
