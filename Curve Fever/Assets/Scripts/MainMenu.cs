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
        if(settingsPanel.ValidatePlayers())
        {
            Debug.Log("Game ready to start");
            settingsPanel.AddPlayers();
            SceneManager.LoadScene("Main");

        }
        else
        {
            Debug.Log("Game not ready to start");
        }
    }

    public void ShowSettingsMenu()
    {
        playButton.SetActive(false);
        settingsPanel.gameObject.SetActive(true);
    }


}

