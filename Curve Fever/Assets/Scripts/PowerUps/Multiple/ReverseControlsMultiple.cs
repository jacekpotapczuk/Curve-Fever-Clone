
using System.Collections;
using UnityEngine;

public class ReverseControlsMultiple : PowerUpMultiple
{
    protected override void StartAction()
    {
        DisableVisuals();
        foreach(Player p in players)
        {
            p.ReverseControls(true);
        }
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        foreach (Player p in players)
        {
            p.ReverseControls(false);
        }
        Destroy(gameObject);
    }
}
