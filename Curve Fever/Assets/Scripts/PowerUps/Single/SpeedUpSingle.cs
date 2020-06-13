using System.Collections;
using UnityEngine;


public class SpeedUpSingle : PowerUpSingle
{

    protected override void StartAction()
    {
        DisableVisuals();
        player.SpeedUp();
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        player.SpeedDown();
        Destroy(gameObject);
    }

}
