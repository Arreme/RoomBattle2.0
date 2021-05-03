using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collide");
        if (other.gameObject.tag.Equals("Player"))
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
        Destroy(this.gameObject);
    }
}
