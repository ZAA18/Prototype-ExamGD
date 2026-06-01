using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public void Play()
    {
        // SceneManager.LoadScene("GamePlayScene");
        SceneManager.LoadScene(1);
    }

    public void OpenSetting()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSetting()
    {

        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void QuitGame()

    {
        Application.Quit();
    }
}
