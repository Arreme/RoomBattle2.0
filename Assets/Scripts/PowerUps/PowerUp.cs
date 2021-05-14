using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PowerUp
{
    bool runPowerUp(GameObject player);

    IEnumerator restorePowerUp(GameObject player);
}
