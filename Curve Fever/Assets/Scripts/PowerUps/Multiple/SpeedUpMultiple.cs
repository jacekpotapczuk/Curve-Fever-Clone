
using System.Collections;
using UnityEngine;

public class SpeedUpMultiple : PowerUpMultiple
{
    protected override void StartAction()
    {
        DisableVisuals();
        foreach(Player p in players)
        {
            p.SpeedUp();
        }
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        foreach (Player p in players)
        {
            p.SpeedDown();
        }
        Destroy(gameObject);
    }
}
