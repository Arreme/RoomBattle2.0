using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public PowerUp _currentPower;
    public bool isPowerRunning;
    int playerIndex;
    private RoombaVFX _vfx;
    void Start()
    {
        _currentPower = new NoPowerUp();
        isPowerRunning = false;
        _vfx = GetComponent<RoombaVFX>();
        _vfx.actiavteLights(false);
        playerIndex = GetComponent<PlayerVariables>().PlayerIndex;
    }

    public void runPowerUp()
    {
        if (!isPowerRunning || _currentPower is NoPowerUp)
        {
            bool check = _currentPower.runPowerUp(gameObject);
            if (check)
            {
                //SOUND OF POWER UP ACTIVATION
                isPowerRunning = true;
                _vfx.actiavteLights(true);
                //ACTIVATE HUD RUNNING EFFECT
                StartCoroutine(_currentPower.restorePowerUp(gameObject));
                _currentPower = new NoPowerUp();
            }
        }
    }

    public void getPower(PowerUp power)
    {
        _currentPower = power;
        _vfx.actiavteLights(true);
    }

    public void setIsPowerUpRunning(bool b)
    {
        isPowerRunning = b;
        HUDManager.Instance.resetPowerUp(playerIndex);
        _vfx.actiavteLights(false);
    }

}
