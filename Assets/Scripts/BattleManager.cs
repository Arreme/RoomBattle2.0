using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private static List<GameObject> _players;
    [SerializeField] GameObject shrink;
    [SerializeField] private float distance = 20f;
    [SerializeField] private float timeForShrink = 5f;
    [SerializeField] private GameObject _restartMenu;
    private float _currTime = 0f;
    private float _spawnerTime = 0f;
    private bool _instantiated = false;

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
        _players = new List<GameObject>();
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
                _controller.GetStunned(0.5f,Random.value -1);
                _controller.changeKnife();
                _target.GetComponent<PlayerVariables>().MaxSpeed = 100;
                CustomPhysics _phy = _target.GetComponent<CustomPhysics>();
                _phy.addForce(new Vector2(direction.x, direction.z), 700);
            }
        }
    }

    private void Update()
    {
        _spawnerTime += Time.deltaTime;
        _currTime += Time.deltaTime;
        if (!_instantiated && _currTime >= timeForShrink)
        {
            int player = Random.Range(0,_players.Count);
            Instantiate(shrink,_players[player].transform.position + new Vector3(0,-0.5f,0),Quaternion.identity,gameObject.transform);
            _instantiated = true;
        }
        if (_players.Count == 1)
        {
            _restartMenu.SetActive(true);
        }

        if(_spawnerTime >= 5)
        {
            bool checkSpawn;
            do
            {
                checkSpawn = gameObject.GetComponent<PowerUpSpawner>().InstantiateRandomObjects();
                _spawnerTime = 0f;
            }
            while(!checkSpawn);
        }
    }
}
