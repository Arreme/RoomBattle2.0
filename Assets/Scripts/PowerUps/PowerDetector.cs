using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDetector : MonoBehaviour
{
    [SerializeField] private GameObject _catBolaPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            if (other.gameObject.GetComponentInParent<PowerUpManager>()._currentPower is NoPowerUp)
            {
                //other.gameObject.GetComponentInParent<PowerUpManager>().getPower(new CatBolaPowerUp(_catBolaPrefab));
                other.gameObject.GetComponentInParent<PowerUpManager>().getPower(new ExtendKnifePowerUp());
                Debug.Log("collide with player");
                destroyPowerUp();
            }
        }
    }

    public void destroyPowerUp()
    {
        Destroy(gameObject);
    }
}
