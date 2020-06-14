using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private class PlayerSpawnInfo
    {
        public string nick;
        public Color color;

        public PlayerSpawnInfo(string nick, Color color)
        {
            this.nick = nick;
            this.color = color;
        }

    }

    public List<Player> players;
    private List<PlayerSpawnInfo> playersToSpawn;

    [SerializeField]
    private Player playerPrefab;

    [SerializeField]
    private string[] inputNames;

    public static PlayerManager Instance;

    private void Awake()
    {
        Instance = this;
        players = new List<Player>();
        playersToSpawn = new List<PlayerSpawnInfo>();

    }

    public void AddPlayer(string nick, Color color)
    {
        if (players.Count > inputNames.Length - 1)
        {
            Debug.Log("Max " + inputNames.Length + " players. Set up more input names to add more players");
            return;
        }

        playersToSpawn.Add(new PlayerSpawnInfo(nick, color));
    }

    public void SpawnPlayers()
    {
        for(int i = 0; i < playersToSpawn.Count; i++)
        {
            Player player = Instantiate(playerPrefab);

            player.SetUp(playersToSpawn[i].nick, playersToSpawn[i].color, new Vector3(0, 0, 0), Color.white, inputNames[i]);
            players.Add(player);
        }
    }

    public List<Player> GetAllPlayersExcept(Player player)
    {
        List<Player> others = new List<Player>();
        foreach(Player p in players)
        {
            if (p != player)
                others.Add(p);
            else
                Debug.Log("Rowna sie");
        }
        return others;
    }
}
