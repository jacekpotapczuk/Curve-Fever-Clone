using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpMultiple : PowerUp
{

    protected List<Player> players;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            players = PlayerManager.Instance.GetAllPlayersExcept(collision.GetComponentInParent<Player>());
            Debug.Log("Players: " + players);
            StartAction();
            Debug.Log("Players count " + players.Count);
            foreach(Player p in players)
            {
                p.AddPowerUpTimer(duration);
            }

        }
    }
}
