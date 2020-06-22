using System.Collections;
using UnityEngine;

public class ReverseControlsSingle : PowerUpSingle
{
    protected override void StartAction()
    {
        DisableVisuals();
        playerBody.ReverseControls(true);
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        playerBody.ReverseControls(false);
        Destroy(gameObject);
    }
}
