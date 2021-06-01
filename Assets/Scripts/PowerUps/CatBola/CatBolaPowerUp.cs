using System.Collections;
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
        player.GetComponent<PowerUpManager>().setIsPowerUpRunning(false);
    }

    public bool runPowerUp(GameObject player)
    {
        bool canInstantiate = true;
        //* He eliminado la layermask, url: https://docs.unity3d.com/ScriptReference/Physics.OverlapBox.html
        Collider[] hitColliders = Physics.OverlapBox(
            player.transform.position + player.transform.forward * 4,
             _bolaDeGato.transform.localScale,
              Quaternion.identity);
        int i = 0;

        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].CompareTag("Wall"))
            {
                canInstantiate = false;
            }
            i++;
        }

        if (canInstantiate)
        {
            GameObject.Instantiate(_bolaDeGato, player.transform.position + player.transform.forward * 4, player.transform.rotation);
            return true;
        }
        else
        {
            return false;
        }
    }
}
