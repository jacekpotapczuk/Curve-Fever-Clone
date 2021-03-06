﻿using System.Collections;
using UnityEngine;

public class ReverseControlsMultiple : PowerUpMultiple
{
    protected override void StartAction()
    {
        DisableVisuals();
        foreach(PlayerBody p in playerBodies)
        {
            p.ReverseControls(true);
        }
        StartCoroutine(EndAction());
    }

    protected override IEnumerator EndAction()
    {
        yield return new WaitForSeconds(duration);

        foreach (PlayerBody p in playerBodies)
        {
            p.ReverseControls(false);
        }
        Destroy(gameObject);
    }
}
