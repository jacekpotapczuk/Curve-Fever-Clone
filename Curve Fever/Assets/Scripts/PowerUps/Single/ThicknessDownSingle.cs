using System.Collections;
using UnityEngine;

public class ThicknessDownSingle : PowerUpSingle
{
    protected override void StartAction()
    {
        DisableVisuals();
        playerBody.ThicknessDown();
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        playerBody.ThicknessUp();
        Destroy(gameObject);
    }
}
