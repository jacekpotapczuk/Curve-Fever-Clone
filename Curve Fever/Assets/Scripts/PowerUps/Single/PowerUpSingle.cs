
using UnityEngine;

public abstract class PowerUpSingle : PowerUp
{

    protected PlayerBody playerBody;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerBody = collision.GetComponentInParent<PlayerBody>();
            StartAction();
            playerBody.AddPowerUpTimer(duration);
        }
    }
}
