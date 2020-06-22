using System.Collections;
using UnityEngine;

public class ThicknessUpSingle : PowerUpSingle
{
    protected override void StartAction()
    {
        DisableVisuals();
        playerBody.ThicknessUp();
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        playerBody.ThicknessDown();
        Destroy(gameObject);
    }
}
