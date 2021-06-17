using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsInitializer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerSpawn;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private TextMeshProUGUI _text;
    private void Start()
    {
        var playerConfig = PlayerConfigManager.Instance._deadPlayers;
        PlayerConfigManager.Instance._endScreen = true;
        int offset = 6 - playerConfig.Count;
        for (int i = 0; i < playerConfig.Count; i++)
        {
            var player = Instantiate(playerPrefab, playerSpawn[i+offset].transform);
            player.GetComponent<InputManager>().InitializePlayer(playerConfig[i]);
        }
        PlayerConfigManager.Instance._endScreen = false;
        if (playerConfig[0].IsIA) _text.SetText("IA "+(playerConfig[playerConfig.Count-1].PlayerIndex+1)+" WINS");
        else _text.SetText("PLAYER " + (playerConfig[playerConfig.Count-1].PlayerIndex + 1) + " WINS");
    }
}