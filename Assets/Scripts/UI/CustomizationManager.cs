using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomizationManager : MonoBehaviour
{
    public static CustomizationManager Instance;

    [SerializeField]
    GameObject[] _playersUI;

    [SerializeField]
    Material[] _materials;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public Material InitializeCamera(int playerIndex)
    {
        return _materials[playerIndex];
    }

    public GameObject getRoomba(int playerIndex)
    {
        return _playersUI[playerIndex];
    }

    internal void putColor(int index, Material[] material)
    {
        _playersUI[index].transform.GetChild(1).GetComponent<MeshRenderer>().material = material[0];
        MeshRenderer[] balloons = _playersUI[index].transform.GetChild(0).GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in balloons) mesh.material = material[1];
    }

    internal void activateHat(int playerIndex, string hat)
    {
        MeshFilter[] meshes = _playersUI[playerIndex].transform.GetChild(1).GetComponentsInChildren<MeshFilter>().Skip(1).ToArray();
        foreach(MeshFilter target in meshes)
        {
            if (target.name.Equals(hat))
            {
                target.GetComponent<MeshRenderer>().enabled = true;
            } else
            {
                target.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    internal void activateKnive(int playerIndex, string hat)
    {
        MeshFilter[] meshes = _playersUI[playerIndex].transform.GetChild(2).GetComponentsInChildren<MeshFilter>().ToArray();
        foreach (MeshFilter target in meshes)
        {
            if (target.name.Equals(hat))
            {
                target.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                target.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
