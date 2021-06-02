using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfig> _configs;
    public static PlayerConfigManager Instance { get; private set; }

    public bool _teamsEnabled = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            _configs = new List<PlayerConfig>();
        }
    }

    public List<PlayerConfig> GetPlayerConfigs()
    {
        return _configs;
    }

    public void ReadyPlayer(int index)
    {
        _configs[index].IsReady = true;
        if (_configs.All(p => p.IsReady == true))
        {
            SceneManager.LoadScene("FinalV3");
        }
    }

    public void SetPlayerColor(int index, Material[] color, Color lightColor,Colors colorEnum)
    {
        _configs[index].colorSelected = colorEnum;
        _configs[index].PlayerMaterial = color[0];
        CustomizationManager.Instance.putColor(index, color);
        _configs[index].ballonMat = color[1];
        _configs[index].lightColor = lightColor;

    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player joined: " + pi.playerIndex);
        pi.transform.SetParent(transform);
        if (!_configs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            _configs.Add(new PlayerConfig(pi));
        }
    }

    public void SetBlueTeams(int playerIndex, bool team)
    {
        _configs[playerIndex].TeamBlue = team;
    }

    public void SetPlayerHat(int playerIndex, string hat)
    {
        CustomizationManager.Instance.activateHat(playerIndex,hat);
        _configs[playerIndex].hatInstance = AssetsLoader.Instance.getHatPrefab(hat);
    }

    internal void SetPlayerKnive(int playerIndex, string knive)
    {
        CustomizationManager.Instance.activateKnive(playerIndex, knive);
        _configs[playerIndex].kniveInstance = AssetsLoader.Instance.getKnifePrefab(knive);
    }
}

public class PlayerConfig
{

    public PlayerConfig(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
        TeamBlue = false;
    }

    public Colors colorSelected { get; set; }
    public PlayerInput Input { get; set; }

    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }

    public bool TeamBlue { get; set; }

    public Color lightColor;
    public Material ballonMat { get; set; }
    public Material PlayerMaterial { get; set; }

    public GameObject hatInstance { get; set; }

    public GameObject kniveInstance { get; set; }
}
