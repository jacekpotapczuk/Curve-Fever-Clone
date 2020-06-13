using System.Collections;
using UnityEngine;

public class ThicknessUpSingle : PowerUpSingle
{
    protected override void StartAction()
    {
        DisableVisuals();
        player.ThicknessUp();
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        player.ThicknessDown();
        Destroy(gameObject);
    }
}
