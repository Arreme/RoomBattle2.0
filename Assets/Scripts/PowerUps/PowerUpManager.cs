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
                AudioManager.Instance._PlaySFX("PowerCat");           
                break;
            case 2:
                AudioManager.Instance._PlaySFX("PowerAceite");               
                break;
            case 3:
                AudioManager.Instance._PlaySFX("PowerBoost");               
                break;
            case 4:
                AudioManager.Instance._PlaySFX("PowerButcher");              
                break;
            case 5:
                AudioManager.Instance._PlaySFX("PowerAfilador");                
                break;
            case 6:
                AudioManager.Instance._PlaySFX("PowerInflate");              
                break;
        }
    }

    public void getPower(PowerUp power)
    {
        
        _currentPower = power;
        _vfx.actiavteLights(true);
        hasPowerUp = true;
        AudioManager.Instance._PlaySFX("PowerUp");
    }

    public void setIsPowerUpRunning(bool b)
    {
        isPowerRunning = b;
        HUDManager.Instance.resetPowerUp(playerIndex);
        _vfx.actiavteLights(false);
        hasPowerUp = false;
    }

}
