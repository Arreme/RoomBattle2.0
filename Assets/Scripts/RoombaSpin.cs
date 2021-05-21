using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaSpin : MonoBehaviour
{
    Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.angularVelocity = new Vector3(0, 2, 0);
    }
}
