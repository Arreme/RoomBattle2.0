using System.Collections;
using UnityEngine;

public class ExtendKnifePowerUp : PowerUp
{
    private GameObject _knife;
    private Vector3 _oldScale;

    public IEnumerator restorePowerUp(GameObject player)
    {
        yield return new WaitForSecondsRealtime(5f);
        _knife.transform.localScale = _oldScale;
        player.GetComponent<PowerUpManager>().setIsPowerUpRunning(false);
    }

    public void runPowerUp(GameObject player)
    {
        _knife = player.transform.Find("Knife").gameObject;
        _oldScale = _knife.transform.localScale;

        var newScale = new Vector3(
            _knife.transform.localScale.x,
            _knife.transform.localScale.y,
            _knife.transform.localScale.z * 2);

        _knife.transform.localScale = newScale;
    }
}