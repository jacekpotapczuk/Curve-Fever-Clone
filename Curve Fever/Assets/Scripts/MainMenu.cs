using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject playButton;

    [SerializeField]
    private SettingsPanelControler settingsPanel;

    public void StartGame()
    {
        if (settingsPanel.ValidatePlayers())
        {
            settingsPanel.AddPlayers();
            SceneManager.LoadScene("Main");
        }
    }

    public void ShowSettingsMenu()
    {
        playButton.SetActive(false);
        settingsPanel.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

