using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public PowerUp _currentPower;
    private bool isPowerRunning;
    int playerIndex;
    void Start()
    {
        _currentPower = new NoPowerUp();
        isPowerRunning = false;
        StartCoroutine(GetComponent<RoombaVFX>().checkForPowerUp(this));
        playerIndex = GetComponent<PlayerVariables>().PlayerIndex;
    }

    public void runPowerUp()
    {
        if (!isPowerRunning)
        {
            isPowerRunning = true;
            bool check = _currentPower.runPowerUp(gameObject);
            if (check)
            {
                StartCoroutine(_currentPower.restorePowerUp(gameObject));
                _currentPower = new NoPowerUp();
            } else
            {
                isPowerRunning = false;
            }
        }
    }

    public void getPower(PowerUp power)
    {
        _currentPower = power;
    }

    public void setIsPowerUpRunning(bool b)
    {
        isPowerRunning = b;
        HUDManager.Instance.resetPowerUp(playerIndex);
    }

}
