using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDetector : MonoBehaviour
{
    [SerializeField] private GameObject _catBolaPrefab;
    [SerializeField] private GameObject _oilPrefab;
    private const int nPowerUps = 7;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            if (other.gameObject.GetComponentInParent<PowerUpManager>()._currentPower is NoPowerUp)
            {
                chooseRandomPU(other);
                destroyPowerUp();
            }
        }
    }

    private void chooseRandomPU(Collider other)
    {
        int random = UnityEngine.Random.Range(1, nPowerUps);
        int playerIndex = other.gameObject.GetComponentInParent<PlayerVariables>().PlayerIndex;
        HUDManager.Instance.SetPowerUp(playerIndex,random);
        switch (random)
        {
            case 1:
                other.gameObject.GetComponentInParent<PowerUpManager>().getPower(new ExtendKnifePowerUp());
                break;
            case 2:
                other.gameObject.GetComponentInParent<PowerUpManager>().getPower(new RunPowerUp());
                break;
            case 3:
                other.gameObject.GetComponentInParent<PowerUpManager>().getPower(new OilPowerUp(_oilPrefab));
                break;
            case 4:
                other.gameObject.GetComponentInParent<PowerUpManager>().getPower(new ButcherPowerUp());
                break;
            case 5:
                other.gameObject.GetComponentInParent<PowerUpManager>().getPower(new CatBolaPowerUp(_catBolaPrefab));
                break;
            case 6:
                other.gameObject.GetComponentInParent<PowerUpManager>().getPower(new InflatePowerUp());
                break;
        }
    }

    public void destroyPowerUp()
    {
        Destroy(gameObject);
    }
}
