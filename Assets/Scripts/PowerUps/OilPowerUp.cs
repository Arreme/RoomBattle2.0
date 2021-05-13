using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilPowerUp : PowerUp
{
    private GameObject oilPrefab;
    private GameObject _oil;

    public OilPowerUp(GameObject prefab)
    {
        oilPrefab = prefab;
    }

    public IEnumerator restorePowerUp(GameObject player)
    {
        yield return new WaitForSecondsRealtime(1f);
        _oil.GetComponent<Collider>().enabled = true;
    }

    public void runPowerUp(GameObject player)
    {
        _oil = GameObject.Instantiate(oilPrefab, player.transform.position, Quaternion.identity);
        _oil.GetComponent<Collider>().enabled = false;
    }
}
