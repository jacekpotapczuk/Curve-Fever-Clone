
using System.Collections;
using UnityEngine;

public class ThicknessUpMultiple : PowerUpMultiple
{
    protected override void StartAction()
    {
        DisableVisuals();
        foreach(Player p in players)
        {
            p.ThicknessUp();
        }
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        foreach (Player p in players)
        {
            p.ThicknessDown();
        }
        Destroy(gameObject);
    }
}
