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

    public List<Player> alivePlayers;
    private List<PlayerSpawnInfo> playersToSpawn;

    [SerializeField]
    private Player playerPrefab;

    [SerializeField]
    private string[] inputNames;

    public static PlayerManager Instance;

    private void Awake()
    {
        Instance = this;
        alivePlayers = new List<Player>();
        playersToSpawn = new List<PlayerSpawnInfo>();

    }

    public void AddPlayer(string nick, Color color)
    {
        if (alivePlayers.Count > inputNames.Length - 1)
        {
            Debug.Log("Max " + inputNames.Length + " players. Set up more input names to add more players");
            return;
        }

        playersToSpawn.Add(new PlayerSpawnInfo(nick, color));
    }

    public void SetPlayerDead(Player player)
    {
        int indexToRemove = -1;
        for (int i = 0; i < alivePlayers.Count; i++)
        {
            if (alivePlayers[i] == player)
                indexToRemove = i;
            else
                player.score += 1;
        }
        alivePlayers.RemoveAt(indexToRemove);
        Debug.Assert(indexToRemove != -1, "Player " + player.nick + " already dead.");

        //TODO: innym rozdać punkty



        
    }

    public void SpawnPlayers()
    {
        for(int i = 0; i < playersToSpawn.Count; i++)
        {
            Player player = Instantiate(playerPrefab);

            float x = Random.Range(-10f, -2f);
            float y = Random.Range(-4f, 4f);
            float angle = Random.Range(0f, 2 * Mathf.PI);
            player.SetUp(playersToSpawn[i].nick, playersToSpawn[i].color, new Vector3(x, y, 0), playersToSpawn[i].color, inputNames[i], angle);
            alivePlayers.Add(player);
        }
    }

    public List<Player> GetAllPlayersExcept(Player player)
    {
        List<Player> others = new List<Player>();
        foreach(Player p in alivePlayers)
        {
            if (p != player)
                others.Add(p);
        }
        return others;
    }
}
