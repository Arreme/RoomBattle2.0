using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private static List<GameObject> _players;
    public static BattleManager Instance;
    [SerializeField] GameObject shrink;
    [SerializeField] private float distance = 20f;
    [SerializeField] private float timeForShrink = 5f;
    [SerializeField] private GameObject _restartMenu;

    public void AddPlayer(GameObject obj)
    {
        _players.Add(obj);
    }

    public static List<GameObject> getPlayers()
    {
        return _players;
    }

    public static void removePlayers(GameObject player)
    {
        _players.Remove(player);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _players = new List<GameObject>();
        StartCoroutine(spawnShrinker());
        InvokeRepeating("createPickUp",10,10);
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
                _controller.GetStunned(0.5f, Random.Range(0, 2) * 2 - 1);
                _controller.changeKnife();
                _target.GetComponent<PlayerVariables>().MaxSpeed = 100;
                CustomPhysics _phy = _target.GetComponent<CustomPhysics>();
                _phy.addForce(new Vector2(direction.x, direction.z), 700);
            }
        }
    }

    private IEnumerator spawnShrinker()
    {
        yield return new WaitForSeconds(timeForShrink);
        int player = Random.Range(0, _players.Count);
        Instantiate(shrink, _players[player].transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity, gameObject.transform);
    }

    private IEnumerator checkForWin()
    {
        for (; ;) 
        {
            if (_players.Count == 1)
            {
                _restartMenu.SetActive(true);
            }
            yield return new WaitForSeconds(.1f);
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
