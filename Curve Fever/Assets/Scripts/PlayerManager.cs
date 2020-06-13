using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private List<Player> players;

    [SerializeField]
    private Player playerPrefab;

    [SerializeField]
    private string[] inputNames;

    private float time = 0f;
    private bool playersStartedDrawing = false;

    public static PlayerManager Instance;

    private void Awake()
    {
        Instance = this;
        players = new List<Player>();

    }

    public void AddPlayer()
    {
        if (players.Count > inputNames.Length - 1)
        {
            Debug.Log("Max " + inputNames.Length + " players. Set up more input names to add more players");
            return;
        }
            
        Player player = Instantiate(playerPrefab);
        player.SetUp(new Vector3(0, 0, 0), Color.white, Color.cyan, inputNames[players.Count]);
        players.Add(player);
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
            AddPlayer();

        if (!playersStartedDrawing && time > 3f)
        {
            playersStartedDrawing = true;

            for (int i = 0; i < players.Count; i++)
            {
                players[i].StartDrawing();
                players[i].AutoDrawingBreaks(true);
                players[i].SetImmortality(false);
            }
        }
    }
}
