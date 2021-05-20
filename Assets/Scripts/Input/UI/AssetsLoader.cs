using UnityEditor;
using UnityEngine;

public class AssetsLoader : MonoBehaviour
{
    public static AssetsLoader Instance;
    public Material[] colorGetter(Colors color)
    {
        Material[] mat = new Material[2];
        switch (color)
        {
            case Colors.Blue:
                mat[0] = (Material)AssetDatabase.LoadAssetAtPath("Assets/Art/Materials/roomba/blue/Blue.mat", typeof(Material));
                mat[1] = (Material)AssetDatabase.LoadAssetAtPath("Assets/Art/Materials/roomba/blue/BlueBalloon.mat", typeof(Material));
                break;
            case Colors.Red:
                mat[0] = (Material)AssetDatabase.LoadAssetAtPath("Assets/Art/Materials/roomba/red/Red.mat", typeof(Material));
                mat[1] = (Material)AssetDatabase.LoadAssetAtPath("Assets/Art/Materials/roomba/red/RedBalloon.mat", typeof(Material));
                break;
            case Colors.Yellow:
                mat[0] = (Material)AssetDatabase.LoadAssetAtPath("Assets/Art/Materials/roomba/yellow/Yellow.mat", typeof(Material));
                mat[1] = (Material)AssetDatabase.LoadAssetAtPath("Assets/Art/Materials/roomba/yellow/YellowBalloon.mat", typeof(Material));
                break;
            case Colors.Green:
                mat[0] = (Material)AssetDatabase.LoadAssetAtPath("Assets/Art/Materials/roomba/green/Green.mat", typeof(Material));
                mat[1] = (Material)AssetDatabase.LoadAssetAtPath("Assets/Art/Materials/roomba/green/GreenBalloon.mat", typeof(Material));
                break;
            case Colors.Purple:
                mat[0] = (Material)AssetDatabase.LoadAssetAtPath("Assets/Art/Materials/roomba/purple/Purple.mat", typeof(Material));
                mat[1] = (Material)AssetDatabase.LoadAssetAtPath("Assets/Art/Materials/roomba/purple/PurpleBalloon.mat", typeof(Material));
                break;
            case Colors.Orange:
                mat[0] = (Material)AssetDatabase.LoadAssetAtPath("Assets/Art/Materials/roomba/orange/Orange.mat", typeof(Material));
                mat[1] = (Material)AssetDatabase.LoadAssetAtPath("Assets/Art/Materials/roomba/orange/OrangeBalloon.mat", typeof(Material));
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
}

