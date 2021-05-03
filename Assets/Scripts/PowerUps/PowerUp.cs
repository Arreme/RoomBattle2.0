using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PowerUp
{
    void runPowerUp(GameObject player);

    IEnumerator restorePowerUp(GameObject player);
}
