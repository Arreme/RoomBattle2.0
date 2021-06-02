using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetsLoader : MonoBehaviour
{
    public static AssetsLoader Instance;

    [SerializeField]
    private Material baseRoomba;
    [SerializeField]
    private Material baseBalloon;

    [SerializeField]
    private Texture[] roombaTextures;
    [SerializeField]
    private Color[] roombaColors;

    [SerializeField]
    private GameObject[] _hatsPrefabs;

    [SerializeField]
    private GameObject[] _knifePrefabs;

    public Material[] colorGetter(Colors color)
    {
        Material[] mat = new Material[2];
        switch (color)
        {
            case Colors.Blue:
                mat[0] = new Material(baseRoomba);
                mat[0].mainTexture = roombaTextures[0];
                mat[1] = new Material(baseBalloon);
                mat[1].color = roombaColors[0];
                break;
            case Colors.Red:
                mat[0] = new Material(baseRoomba);
                mat[0].mainTexture = roombaTextures[1];
                mat[1] = new Material(baseBalloon);
                mat[1].color = roombaColors[1];
                break;
            case Colors.Yellow:
                mat[0] = new Material(baseRoomba);
                mat[0].mainTexture = roombaTextures[2];
                mat[1] = new Material(baseBalloon);
                mat[1].color = roombaColors[2];
                break;
            case Colors.Green:
                mat[0] = new Material(baseRoomba);
                mat[0].mainTexture = roombaTextures[3];
                mat[1] = new Material(baseBalloon);
                mat[1].color = roombaColors[3];
                break;
            case Colors.Purple:
                mat[0] = new Material(baseRoomba);
                mat[0].mainTexture = roombaTextures[4];
                mat[1] = new Material(baseBalloon);
                mat[1].color = roombaColors[4];
                break;
            case Colors.Orange:
                mat[0] = new Material(baseRoomba);
                mat[0].mainTexture = roombaTextures[5];
                mat[1] = new Material(baseBalloon);
                mat[1].color = roombaColors[5];
                break;
        }
        return mat;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject getHatPrefab(string hat)
    {
        foreach(GameObject target in _hatsPrefabs)
        {
            if (target.name.Equals(hat)) return target;
        }
        return null;
    }

    public GameObject getKnifePrefab(string knife)
    {
        foreach (GameObject target in _knifePrefabs)
        {
            if (target.name.Equals(knife)) return target;
        }
        return null;
    }
}

