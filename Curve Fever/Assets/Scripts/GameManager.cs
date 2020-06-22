using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameManager Instance;

    private GameStatus gameStatus;

    private int scoreToWin;

    private float betweenRoundsBreakTime = 3f;
    private float betweenRoundsBreakTimeLeft;
    private float roundStartWaitTime = 4f;
    private float roundStartWaitTimeLeft;

    [SerializeField]
    private TextMeshProUGUI countdownText;

    [SerializeField]
    private TextMeshProUGUI roundWinnerText;

    [SerializeField]
    private GameObject endGameScreen;

    private void Awake()
    {
        Instance = this;
        gameStatus = GameStatus.Initialization;
    }

    private void Start()
    {
        scoreToWin = PlayerManager.Instance.players.Count * PlayerManager.Instance.players.Count * 2;
        ScoreRankingController.Instance.SetMaxScore(scoreToWin);
    }

    private void Update()
    {
        if (gameStatus == GameStatus.Initialization)
            HandleRoundInitialization();
        else if (gameStatus == GameStatus.PreRoundWaiting)
            HandlePreRoundWaiting();
        else if (gameStatus == GameStatus.PreRound)
            HandlePreRound();
        else if (gameStatus == GameStatus.Round && PlayerManager.Instance.alivePlayers.Count <= 1)
            HandleRoundEnd();
        else if (gameStatus == GameStatus.AfterRound)
            HandleAfterRound();
        else if (gameStatus == GameStatus.End)
            HandleGameEnd();
    }

    private void HandleRoundInitialization()
    {
        endGameScreen.SetActive(false);
        gameStatus = GameStatus.PreRoundWaiting;
        roundStartWaitTimeLeft = roundStartWaitTime;

        PlayerManager.Instance.SpawnPlayers();
        foreach (Player p in PlayerManager.Instance.players)
        {
            p.body.SetImmortality(true);
            p.body.SetMovement(false);
        }
        ScoreRankingController.Instance.OnNewRound();
    }

    private void HandlePreRoundWaiting()
    {
        roundStartWaitTimeLeft -= Time.deltaTime;
        if (roundStartWaitTimeLeft >= 0)
        {
            countdownText.gameObject.SetActive(true);
            if (roundStartWaitTimeLeft <= 1f)
            {
                countdownText.text = "GO";
            }
            else
            {
                int timeLeft = Mathf.FloorToInt(roundStartWaitTimeLeft);
                countdownText.text = timeLeft.ToString();
            }
        }
        else
        {
            countdownText.gameObject.SetActive(false);
            gameStatus = GameStatus.PreRound;
        }
    }

    private void HandlePreRound()
    {
        gameStatus = GameStatus.Round;

        foreach (Player p in PlayerManager.Instance.players)
        {
            p.body.SetImmortality(false);
            p.body.SetMovement(true);
            p.body.StartDrawing();
            p.body.AutoDrawingBreaks(true);
        }
        PowerUpManager.Instance.AutoSpawn(true);
    }

    private void HandleRoundEnd()
    {
        gameStatus = GameStatus.AfterRound;
        PowerUpManager.Instance.AutoSpawn(false);
        PowerUpManager.Instance.ClearPowerUps();
        PlayerManager.Instance.alivePlayers[0].body.SetMovement(false);

        roundWinnerText.gameObject.SetActive(true);
        roundWinnerText.text = PlayerManager.Instance.alivePlayers[0].nick + " WON THE ROUND";

        betweenRoundsBreakTimeLeft = betweenRoundsBreakTime;
    }
    
    private void HandleAfterRound()
    {
        betweenRoundsBreakTimeLeft -= Time.deltaTime;
        if (betweenRoundsBreakTimeLeft <= 0f)
        {
            bool gameFinsished = false;
            foreach (Player player in PlayerManager.Instance.players)
            {
                if (player.score >= scoreToWin)
                {
                    gameFinsished = true; // later: add check if there is a draw
                    break;
                }
            }
            roundWinnerText.gameObject.SetActive(false);
            if (gameFinsished)
                gameStatus = GameStatus.End;
            else
            {
                PlayerManager.Instance.DespawnPlayers();
                gameStatus = GameStatus.Initialization;
            }
        }
    }

    private void HandleGameEnd()
    {
        endGameScreen.GetComponentInChildren<TextMeshProUGUI>().text = PlayerManager.Instance.players[0].nick.ToString() + " WON THE GAME!";
        endGameScreen.GetComponentInChildren<TextMeshProUGUI>().color = PlayerManager.Instance.players[0].color;
        endGameScreen.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
