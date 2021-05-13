using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPowerUp : PowerUp
{
    public void runPowerUp(GameObject player)
    {
        player.GetComponent<PowerUpManager>().setIsPowerUpRunning(false);
    }

    public IEnumerator restorePowerUp(GameObject player)
    {
        yield return new WaitForSecondsRealtime(0f);
    }
}
