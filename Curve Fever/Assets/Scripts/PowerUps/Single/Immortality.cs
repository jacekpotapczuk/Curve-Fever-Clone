using System.Collections;
using UnityEngine;

public class Immortality : PowerUpSingle
{
    protected override void StartAction()
    {
        DisableVisuals();
        player.SetImmortality(true);
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        player.SetImmortality(false);
        Destroy(gameObject);
    }
}
