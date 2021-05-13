using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendKnifePowerUp : PowerUp
{
    private GameObject _knife;

    public IEnumerator restorePowerUp(GameObject player)
    {
        yield return new WaitForSecondsRealtime(1f);
        _knife.transform.localScale = _knife.transform.localScale / 2;
    }

    public void runPowerUp(GameObject player)
    {
        _knife = player.transform.Find("Knife").gameObject;

        _knife.transform.localScale = _knife.transform.localScale * 2;
    }
}