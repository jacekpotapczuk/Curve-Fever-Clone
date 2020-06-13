using System.Collections;
using UnityEngine;

public class ReverseControlsSingle : PowerUpSingle
{
    protected override void StartAction()
    {
        DisableVisuals();
        player.ReverseControls(true);
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        player.ReverseControls(false);
        Destroy(gameObject);
    }
}
