using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private List<GameObject> _players;
    private float distance = 20f;

    public void Awake()
    {
        _players = new List<GameObject>();
    }

    public void AddPlayer(GameObject obj)
    {
        _players.Add(obj);
    }

    public void Explosion(GameObject obj)
    {
        foreach (GameObject _target in _players)
        {
            if (ReferenceEquals(obj, _target)) continue;
            
            if (Vector3.Distance(_target.transform.position, obj.transform.position) < distance)
            {
                Vector3 direction = Vector3.Normalize(_target.transform.position - obj.transform.position);
                _target.GetComponent<NewRoombaController>().GetStunned(0.5f,Random.value -1);
                _target.GetComponent<PlayerVariables>().MaxSpeed = 100;
                CustomPhysics _phy = _target.GetComponent<CustomPhysics>();
                _phy.ResetVelocity();
                _phy.addForce(new Vector2(direction.x, direction.z), 700);
            }
        }
    }
}
