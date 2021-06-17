using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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
        Debug.Log("Illo que empieza esto");
        StartCoroutine(gameStart());
        /*
        var result = playerSpawn;
        result.Shuffle();
        var playerConfig = PlayerConfigManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfig.Length; i++)
        {
            var player = Instantiate(playerPrefab, result[i]);
            _manager.AddPlayer(player);
            player.GetComponent<InputManager>().InitializePlayer(playerConfig[i]);
        }
        */
    }

    public IEnumerator gameStart()
    {
        yield return new WaitForSecondsRealtime(10f);
        var result = playerSpawn;
        result.Shuffle();
        var playerConfig = PlayerConfigManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfig.Length; i++)
        {
            var player = Instantiate(playerPrefab, result[i]);
            _manager.AddPlayer(player);
            player.GetComponent<InputManager>().InitializePlayer(playerConfig[i]);
        }
    }

}

static class Shuffler
{
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random a = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = a.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
