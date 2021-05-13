using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherPowerUp : PowerUp
{
    private GameObject _butcher;
    public IEnumerator restorePowerUp(GameObject player)
    {
        yield return new WaitForSecondsRealtime(10f);
        _butcher.SetActive(false);
    }

    public void runPowerUp(GameObject player)
    {
        _butcher = player.transform.Find("Butcher").gameObject;
        _butcher.SetActive(true);
    }
}
