
using UnityEngine;

public abstract class PowerUpSingle : PowerUp
{

    protected Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.GetComponentInParent<Player>();
            StartAction();
            player.AddPowerUpTimer(duration);
        }
    }
}
