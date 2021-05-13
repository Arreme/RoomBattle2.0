using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    private Vector3 Min;
    private Vector3 Max;
    private float _xAxis;
    private float _yAxis;
    private float _zAxis; //If you need this, use it
    private Vector3 _randomPosition;
    public bool _canInstantiate;

    public GameObject _puPrefab;
    private void Start()
    {
        SetRanges();
    }

    private void GenerateRandomPosition()
    {
        _xAxis = UnityEngine.Random.Range(Min.x, Max.x);
        _yAxis = UnityEngine.Random.Range(Min.y, Max.y);
        _zAxis = UnityEngine.Random.Range(Min.z, Max.z);
        _randomPosition = new Vector3(_xAxis, _yAxis, _zAxis);
    }

    //Here put the ranges where your object will appear, or put it in the inspector.
    private void SetRanges()
    {
        //* Hay que modificar estos valores
        Min = new Vector3(-35.2f, 0.798f, -1.7f); //Random value.
        Max = new Vector3(5.4f, 0.798f, 24.5f); //Another ramdon value, just for the example.
    }
    public bool InstantiateRandomObjects()
    {
        GenerateRandomPosition();
        _canInstantiate = true;
        //* He eliminado la layermask, url: https://docs.unity3d.com/ScriptReference/Physics.OverlapBox.html
        Collider[] hitColliders = Physics.OverlapBox(_randomPosition, _puPrefab.transform.localScale * 5, Quaternion.identity);
        int i = 0;

        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].CompareTag("Wall"))
            {
                _canInstantiate = false;
            }
            i++;
        }

        if (_canInstantiate)
        {
            Instantiate(_puPrefab, _randomPosition, Quaternion.identity);
        }
        else
        {
        }

        return _canInstantiate;
    }
}
