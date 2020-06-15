
using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance;


    private float time = 0f;
    private bool playersStartedDrawing = false;
    private bool playersSpawned = false;

    private GameStatus gameStatus;
    private bool preRoundActivated = false;

    public int pointsToWin = 10;

    private void Awake()
    {
        Instance = this;
        gameStatus = GameStatus.PreRound;
    }


    private void Update()
    {
        time += Time.deltaTime;

        if (gameStatus == GameStatus.PreRound && !preRoundActivated)
        {
            preRoundActivated = true;
            PlayerManager.Instance.SpawnPlayers();
            for (int i = 0; i < PlayerManager.Instance.alivePlayers.Count; i++)
            {
                PlayerManager.Instance.alivePlayers[i].SetImmortality(true);
                PlayerManager.Instance.alivePlayers[i].SetMovement(false);

            }
            ShowStartCountdown();
        }
        else if (time >= 3f && gameStatus == GameStatus.PreRound)
        {
            gameStatus = GameStatus.Round;
            for (int i = 0; i < PlayerManager.Instance.alivePlayers.Count; i++)
            {
                PlayerManager.Instance.alivePlayers[i].SetImmortality(false);
                PlayerManager.Instance.alivePlayers[i].SetMovement(true);
                PlayerManager.Instance.alivePlayers[i].StartDrawing();
                PlayerManager.Instance.alivePlayers[i].AutoDrawingBreaks(true);
            }
            PowerUpManager.Instance.AutoSpawn(true);
        }
        else if (gameStatus == GameStatus.Round && PlayerManager.Instance.alivePlayers.Count <= 1)
        {
            gameStatus = GameStatus.AfterRound;
            PowerUpManager.Instance.AutoSpawn(false);
            PowerUpManager.Instance.ClearPowerUps();
            PlayerManager.Instance.alivePlayers[0].SetMovement(false);

            Debug.Log("------Koniec rundy-------");

            ShowEndOfRoundInfo();
            // reset wszystkich rzeczy 
        }


    }

    private void ShowStartCountdown()
    {

    }

    private void ShowEndOfRoundInfo()
    {

    }


}
