using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPowerUp : PowerUp
{
    PlayerVariables _pVar;
    public IEnumerator restorePowerUp(GameObject player)
    {
        yield return new WaitForSecondsRealtime(5f);
        _pVar._baseMaxSpeed = _pVar._baseMaxSpeed / 1.5f;
        _pVar._normalSpeed = _pVar._normalSpeed / 2f;
    }

    public void runPowerUp(GameObject player)
    {
        _pVar = player.GetComponent<PlayerVariables>();
        _pVar._baseMaxSpeed = _pVar._baseMaxSpeed * 1.5f;
        _pVar._normalSpeed = _pVar._normalSpeed * 2f;
    }


}