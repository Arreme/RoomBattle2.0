using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBolaPowerUp : PowerUp
{
    private GameObject _bolaDeGato;
    public CatBolaPowerUp(GameObject pref)
    {
        _bolaDeGato = pref;
    }

    public IEnumerator restorePowerUp(GameObject player)
    {
        yield return new WaitForSecondsRealtime(0);
    }

    public void runPowerUp(GameObject player)
    {
        
    }
}
