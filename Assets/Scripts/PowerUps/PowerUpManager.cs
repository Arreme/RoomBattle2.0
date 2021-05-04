using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public PowerUp _currentPower;
    void Start()
    {
        _currentPower = new NoPowerUp();
    }

    public void runPowerUp()
    {
        _currentPower.runPowerUp(gameObject);
        StartCoroutine(_currentPower.restorePowerUp(gameObject));
        _currentPower = new NoPowerUp();
    }

    public void getPower(PowerUp power)
    {
        _currentPower = power;
    }
}
