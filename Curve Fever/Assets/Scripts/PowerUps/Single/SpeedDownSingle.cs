
using System.Collections;
using UnityEngine;

public class SpeedDownSingle : PowerUpSingle
{
    protected override void StartAction()
    {
        DisableVisuals();
        playerBody.SpeedDown();
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        playerBody.SpeedUp();
        Destroy(gameObject);
    }
}
