using TMPro;
using UnityEngine;

public class SettingsPanelControler : MonoBehaviour
{
    [SerializeField]
    private PlayerSetting[] playerFields;

    [SerializeField]
    private TextMeshProUGUI errorText;

    private void Start()
    {
        for (int i = 0; i < playerFields.Length; i++)
        {
            playerFields[i].ChangeText("Player " + (i + 1), Color.white);
        }
    }

    public bool ValidatePlayers()
    {
        int activePlayerCount = 0;

        for (int i = 0; i < playerFields.Length; i++)
        {
            if (playerFields[i].IsActive)
                activePlayerCount += 1;
        }
        if (activePlayerCount >= 2) 
            return true;
        {
            errorText.text = "Not enough players. Activate at least two.";
            return false;
        }
    }

    public void AddPlayers()
    {
        for (int i = 0; i < playerFields.Length; i++)
        {
            if (playerFields[i].IsActive)
            {
                string nick = playerFields[i].GetNick();
                Color color = playerFields[i].GetColor();
                PlayerManager.Instance.AddPlayer(nick, color);
            }
        }
    }
}
