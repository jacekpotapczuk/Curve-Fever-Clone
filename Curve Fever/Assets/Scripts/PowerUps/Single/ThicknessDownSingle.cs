using System.Collections;
using UnityEngine;

public class ThicknessDownSingle : PowerUpSingle
{
    protected override void StartAction()
    {
        DisableVisuals();
        player.ThicknessDown();
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        player.ThicknessUp();
        Destroy(gameObject);
    }
}
