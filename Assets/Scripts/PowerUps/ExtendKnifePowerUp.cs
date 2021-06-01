using System.Collections;
using UnityEngine;

public class ExtendKnifePowerUp : PowerUp
{
    private GameObject _knife;
    private Vector3 _oldScale;
    private Vector3 _oldPos;

    public IEnumerator restorePowerUp(GameObject player)
    {
        yield return new WaitForSecondsRealtime(5f);
        _knife.transform.localScale = _oldScale;
        _knife.transform.localPosition = _oldPos;
        player.GetComponent<PowerUpManager>().setIsPowerUpRunning(false);
    }

    public bool runPowerUp(GameObject player)
    {
        _knife = player.transform.Find("Knife").gameObject;
        _oldScale = _knife.transform.localScale;
        _oldPos = _knife.transform.localPosition;
        var newScale = new Vector3(
            _knife.transform.localScale.x,
            _knife.transform.localScale.y,
            _knife.transform.localScale.z * 2);

        _knife.transform.localScale = newScale;
        _knife.transform.localPosition = new Vector3(_knife.transform.localPosition.x,_knife.transform.localPosition.y,2.3f);
        return true;
    }
}