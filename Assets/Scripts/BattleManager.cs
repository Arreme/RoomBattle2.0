using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    private static List<GameObject> _players;
    public static BattleManager Instance;
    [SerializeField] private float distance = 20f;
    [SerializeField] private GameObject _restartMenu;

    public int _redAlive = 0;
    public int _blueAlive = 0;

    public void AddPlayer(GameObject obj)
    {
        _players.Add(obj);
    }

    public static List<GameObject> getPlayers()
    {
        return _players;
    }

    public void removePlayers(GameObject player)
    {
        PlayerConfigManager.Instance._deadPlayers.Add(PlayerConfigManager.Instance.GetPlayerConfigs()[player.GetComponent<InputManager>()._playerConfig.PlayerIndex]);
        _players.Remove(player);
        if (player.GetComponent<InputManager>()._teamBlue)
        {
            _blueAlive -= 1;
        }
        else
        {
            _redAlive -= 1;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _players = new List<GameObject>();
        InvokeRepeating("createPickUp", 10, 10);
        StartCoroutine(checkForWin());
    }

    public void Explosion(GameObject obj)
    {
        foreach (GameObject _target in _players)
        {
            if (ReferenceEquals(obj, _target)) continue;

            if (Vector3.Distance(_target.transform.position, obj.transform.position) < distance)
            {
                Vector3 direction = Vector3.Normalize(_target.transform.position - obj.transform.position);
                NewRoombaController _controller = _target.GetComponent<NewRoombaController>();
                _target.GetComponent<PlayerVariables>().MaxSpeed = 100;
                _controller.GetStunned(1f, new Vector2(direction.x, direction.z), 2000);
                StartCoroutine(_controller.changeKnife());
            }
        }
    }

    private IEnumerator loadSceneAsync()
    {
        yield return new WaitForSecondsRealtime(2f);
        Camera.main.GetComponent<Animation>().Play("Transition");
        SceneManager.LoadSceneAsync(4);
        yield return null;
    }

    private IEnumerator checkForWin()
    {
        yield return new WaitForSeconds(10f);
        if (PlayerConfigManager.Instance._teamsEnabled)
        {
            for (; ; )
            {
                if (_redAlive == 0)
                {
                    StartCoroutine(loadSceneAsync());
                    break;
                }
                else if (_blueAlive == 0)
                {
                    StartCoroutine(loadSceneAsync());
                    break;
                }
                yield return new WaitForSeconds(.1f);
            }
        }
        else
        {
            for (; ; )
            {
                if (_players.Count <= 1)
                {
                    PlayerConfigManager.Instance._deadPlayers.Add(PlayerConfigManager.Instance.GetPlayerConfigs()[_players[0].GetComponent<InputManager>()._playerConfig.PlayerIndex]);
                    StartCoroutine(loadSceneAsync());
                    break;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }


    }

    private void createPickUp()
    {
        bool checkSpawn;
        do
        {
            checkSpawn = gameObject.GetComponent<PowerUpSpawner>().InstantiateRandomObjects();
        }
        while (!checkSpawn);
    }
}
