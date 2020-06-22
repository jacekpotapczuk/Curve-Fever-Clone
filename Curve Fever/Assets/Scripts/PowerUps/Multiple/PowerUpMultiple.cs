using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpMultiple : PowerUp
{

    protected List<PlayerBody> playerBodies;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerBodies = PlayerManager.Instance.GetAllPlayerBodiesExcept(collision.GetComponentInParent<PlayerBody>());
            Debug.Log("Players: " + playerBodies);
            StartAction();
            Debug.Log("Players count " + playerBodies.Count);
            foreach(PlayerBody p in playerBodies)
            {
                p.AddPowerUpTimer(duration);
            }

        }
    }
}
