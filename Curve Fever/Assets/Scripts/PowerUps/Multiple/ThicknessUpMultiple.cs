
using System.Collections;
using UnityEngine;

public class ThicknessUpMultiple : PowerUpMultiple
{
    protected override void StartAction()
    {
        DisableVisuals();
        foreach(PlayerBody p in playerBodies)
        {
            p.ThicknessUp();
        }
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        foreach (PlayerBody p in playerBodies)
        {
            p.ThicknessDown();
        }
        Destroy(gameObject);
    }
}
