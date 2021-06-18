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
    private Animation anim;
    public GameObject door;

    private void Start()
    {
        door.SetActive(false);
        AudioManager.Instance._StopMusic();
        anim = Camera.main.GetComponent<Animation>();
        if (PlayerConfigManager.Instance.runAnimation)
        {
            AudioManager.Instance._PlayMusic("InGame", true);
            anim.Play("levelStart2");
            StartCoroutine(gameStart(10f));
        }
        else
        {
            AudioManager.Instance._PlayMusic("Restart", true);
            StartCoroutine(gameStart(0f));
        }
    }

    public IEnumerator gameStart(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        var result = playerSpawn;
        result.Shuffle();
        var playerConfig = PlayerConfigManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfig.Length; i++)
        {
            var player = Instantiate(playerPrefab, result[i]);
            _manager.AddPlayer(player);
            player.GetComponent<InputManager>().InitializePlayer(playerConfig[i]);
        }
        PlayerConfigManager.Instance.runAnimation = false;
        door.SetActive(true);
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
