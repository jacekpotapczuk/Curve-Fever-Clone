using System.Collections;
using UnityEngine;

public class ThicknessDownMultiple : PowerUpMultiple
{
    protected override void StartAction()
    {
        DisableVisuals();
        foreach(Player p in players)
        {
            p.ThicknessDown();
        }
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        foreach (Player p in players)
        {
            p.ThicknessUp();
        }
        Destroy(gameObject);
    }
}
