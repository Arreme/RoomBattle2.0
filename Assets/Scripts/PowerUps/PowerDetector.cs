using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            if (other.gameObject.GetComponentInParent<PowerUpManager>()._currentPower is NoPowerUp)
            {
                other.gameObject.GetComponentInParent<PowerUpManager>().getPower(new RunPowerUp());
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
