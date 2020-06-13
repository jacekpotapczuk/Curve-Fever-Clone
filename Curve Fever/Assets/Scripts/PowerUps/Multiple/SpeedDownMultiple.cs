
using System.Collections;
using UnityEngine;

public class SpeedDownMultiple : PowerUpMultiple
{
    protected override void StartAction()
    {
        DisableVisuals();
        foreach(Player p in players)
        {
            p.SpeedDown();
        }
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        foreach (Player p in players)
        {
            p.SpeedUp();
        }
        Destroy(gameObject);
    }
}
