using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField]
    private Transform[] playerSpawn;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private BattleManager _manager;
    private void Start()
    {
        var playerConfig = PlayerConfigManager.Instance.GetPlayerConfigs().ToArray();
        DetectCollision.OnHit += _manager.Explosion;
        for (int i = 0; i < playerConfig.Length; i++)
        {
            //TODO: Get a random spawn from list
            var player = Instantiate(playerPrefab, playerSpawn[i]);
            _manager.AddPlayer(player);
            player.GetComponent<InputManager>().InitializePlayer(playerConfig[i]);
        }
    }
}
