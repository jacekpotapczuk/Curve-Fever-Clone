using System.Collections;
using UnityEngine;

public class Immortality : PowerUpSingle
{
    protected override void StartAction()
    {
        DisableVisuals();
        playerBody.SetImmortality(true);
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        playerBody.SetImmortality(false);
        Destroy(gameObject);
    }
}
