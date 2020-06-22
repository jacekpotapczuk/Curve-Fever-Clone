using System.Collections;
using UnityEngine;


public class SpeedUpSingle : PowerUpSingle
{

    protected override void StartAction()
    {
        DisableVisuals();
        playerBody.SpeedUp();
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        playerBody.SpeedDown();
        Destroy(gameObject);
    }

}
