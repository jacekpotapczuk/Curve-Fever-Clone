
using System.Collections;
using UnityEngine;

public class SpeedDownSingle : PowerUpSingle
{
    protected override void StartAction()
    {
        DisableVisuals();
        player.SpeedDown();
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        player.SpeedUp();
        Destroy(gameObject);
    }
}
