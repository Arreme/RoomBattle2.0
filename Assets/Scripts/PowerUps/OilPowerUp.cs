using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilPowerUp : PowerUp
{
    private GameObject _oilPrefab;
    private GameObject _oil;

    public OilPowerUp(GameObject prefab)
    {
        _oilPrefab = prefab;
    }

    public int identify()
    {
        return 2;
    }

    public IEnumerator restorePowerUp(GameObject player)
    {
        yield return new WaitForSecondsRealtime(1.5f);
        _oil.GetComponent<Collider>().enabled = true;
        player.GetComponent<PowerUpManager>().setIsPowerUpRunning(false);
    }

    public bool runPowerUp(GameObject player)
    {
        _oil = GameObject.Instantiate(_oilPrefab, player.transform.position, player.transform.rotation);
        _oil.GetComponent<Collider>().enabled = false;

        return true;
    }
}
