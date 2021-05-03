using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPowerUp : PowerUp
{
    public void runPowerUp(GameObject player)
    {
        Debug.Log("Doing nothing");
    }

    public IEnumerator restorePowerUp(GameObject player)
    {
        yield return new WaitForSecondsRealtime(5f);
        Debug.Log("Still Nothing");
    }
}
