using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public PowerUp _currentPower;
    private bool isPowerRunning;
    void Start()
    {
        _currentPower = new NoPowerUp();
        isPowerRunning = false;
    }

    public void runPowerUp()
    {
        if (!isPowerRunning)
        {
            isPowerRunning = true;
            _currentPower.runPowerUp(gameObject);
            StartCoroutine(_currentPower.restorePowerUp(gameObject));
            _currentPower = new NoPowerUp();
        }
    }

    public void getPower(PowerUp power)
    {
        _currentPower = power;
    }

    public void setIsPowerUpRunning(bool b)
    {
        Debug.Log("Not running");
        isPowerRunning = b;
    }
}
