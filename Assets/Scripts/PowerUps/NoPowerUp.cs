using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPowerUp : PowerUp
{
    public bool runPowerUp(GameObject player)
    {
        return false;
    }

    public IEnumerator restorePowerUp(GameObject player)
    {
        yield return new WaitForSecondsRealtime(0f);
    }
}
