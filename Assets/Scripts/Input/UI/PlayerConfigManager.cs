using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

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
            GameObject layout = GameObject.Find("MainLayout");
            if (layout != null) layout.SetActive(false);
            GameObject teamCanvas = GameObject.Find("TeamSelection");
            if (layout != null) teamCanvas.GetComponent<Canvas>().enabled = true;
            teamCanvas.GetComponentInChildren<InputSystemUIInputModule>().enabled = true;
        }
    }

    public void SetPlayerColor(int index, Material[] color, Color lightColor)
    {
        _configs[index].PlayerMaterial = color[0];
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
}

public class PlayerConfig
{

    public PlayerConfig(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
        Team = pi.playerIndex;
    }

    public PlayerInput Input { get; set; }

    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }

    public int Team { get; set; }

    public Color lightColor;
    public Material ballonMat {get; set;}
    public Material PlayerMaterial { get; set; }
}
