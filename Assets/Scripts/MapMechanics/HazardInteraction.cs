using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardInteraction : Interactable
{
    private GameObject _hazard;
    private float _hazardHP = 60;
    public void runInteraction(GameObject gameObject)
    {
        _hazardHP -= gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude;
        Debug.Log(_hazardHP);
        if (_hazardHP <= 0)
        {
            GameObject.Destroy(_hazard);
        }
    }

    public HazardInteraction(GameObject hazard)
    {
        _hazard = hazard;
    }
}
