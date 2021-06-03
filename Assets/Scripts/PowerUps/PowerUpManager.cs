using UnityEngine;
using System.Collections;
using JSAM;

public class PowerUpManager : MonoBehaviour
{
    public PowerUp _currentPower;
    private bool isPowerRunning;
    public bool hasPowerUp;
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
        if (!isPowerRunning)
        {
            bool check = _currentPower.runPowerUp(gameObject);
            if (check)
            {
                soundOn(_currentPower.identify());
                isPowerRunning = true;
                _vfx.actiavteLights(true);
                //ACTIVATE HUD RUNNING EFFECT
                StartCoroutine(_currentPower.restorePowerUp(gameObject));
                _currentPower = new NoPowerUp();
            }
        }
    }

    private void soundOn(int i)
    {
        switch (i)
        {

            case 1:
                JSAM.AudioManager.PlaySound(Sounds.powerup_butcher, transform);
                break;
            case 2:
                JSAM.AudioManager.PlaySound(Sounds.powerup_oil, transform);
                break;
            case 3:
                JSAM.AudioManager.PlaySound(Sounds.powerup_butcher, transform);
                break;
            case 4:
                JSAM.AudioManager.PlaySound(Sounds.powerup_butcher, transform);
                break;
            case 5:
                JSAM.AudioManager.PlaySound(Sounds.powerup_viagra, transform);
                break;
            case 6:
                JSAM.AudioManager.PlaySound(Sounds.powerup_inflar, transform);
                break;
        }
    }

    public void getPower(PowerUp power)
    {
        _currentPower = power;
        _vfx.actiavteLights(true);
        hasPowerUp = true;
    }

    public void setIsPowerUpRunning(bool b)
    {
        isPowerRunning = b;
        HUDManager.Instance.resetPowerUp(playerIndex);
        _vfx.actiavteLights(false);
        hasPowerUp = false;
    }

}
