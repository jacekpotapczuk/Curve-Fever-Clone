
using System.Collections;
using UnityEngine;

public class SpeedUpMultiple : PowerUpMultiple
{
    protected override void StartAction()
    {
        DisableVisuals();
        foreach(PlayerBody p in playerBodies)
        {
            p.SpeedUp();
        }
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        foreach (PlayerBody p in playerBodies)
        {
            p.SpeedDown();
        }
        Destroy(gameObject);
    }
}
