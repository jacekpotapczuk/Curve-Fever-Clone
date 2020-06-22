using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public List<Player> players;
    public List<Player> alivePlayers;

    [SerializeField]
    PlayerBody playerBodyPrefab;

    [SerializeField]
    private string[] inputNames;

    private void Awake()
    {
        Instance = this;
        players = new List<Player>();
        alivePlayers = new List<Player>();
    }

    public void AddPlayer(string nick, Color color)
    {
        if (players.Count >= inputNames.Length)
            Debug.LogError("Can't add more players. No input axes left.");
        players.Add(new Player(nick, color, inputNames[players.Count]));
    }


    public void SpawnPlayers()
    {
        foreach(Player p in players)
        {
            PlayerBody pb = Instantiate(playerBodyPrefab);

            float x = Random.Range(-8f, 0f);
            float y = Random.Range(-2f, 2f);
            float angle = Random.Range(0f, 2 * Mathf.PI);

            pb.SetUp(p.nick, p.color, new Vector3(x, y, 0f), p.color, p.inputName, angle, p);
            p.body = pb;
            alivePlayers.Add(p);
        }
    }

    public void DespawnPlayers()
    {
        foreach(Player p in players)
        {
            Destroy(p.body.gameObject);
        }
        alivePlayers.Clear();
    }

    public void OnPlayerDead(Player player)
    {
        int indexToRemove = -1;
        for (int i = 0; i < alivePlayers.Count; i++)
        {
            if (alivePlayers[i] == player)
            {
                indexToRemove = i;
            }
            else
            {
                alivePlayers[i].score += 1;
            }
        }
        alivePlayers.RemoveAt(indexToRemove);
        ScoreRankingController.Instance.OnPlayerDead(player);
    }

    public List<PlayerBody> GetAllPlayerBodiesExcept(PlayerBody playerBody)
    {
        List<PlayerBody> others = new List<PlayerBody>();
        foreach (Player p in alivePlayers)
        {
            if (p.body != playerBody)
                others.Add(p.body);
        }
        return others;
    }
}
