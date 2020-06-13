
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance;


    private float time = 0f;
    private bool playersStartedDrawing = false;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
            PlayerManager.Instance.AddPlayer();
        if (Input.GetKeyDown(KeyCode.Q))
            PowerUpManager.Instance.SpawnRandomizedPowerUp();


        if (!playersStartedDrawing && time > 3f)
        {
            playersStartedDrawing = true;

            for (int i = 0; i < PlayerManager.Instance.players.Count; i++)
            {
                PlayerManager.Instance.players[i].StartDrawing();
                PlayerManager.Instance.players[i].AutoDrawingBreaks(true);
                PlayerManager.Instance.players[i].SetImmortality(false);
            }
        }
    }

}
