using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Chasing,
        UsingPower,
        Boosting,
        PickingPowerUp,
    }

    private List<GameObject> _players;
    private NavMeshPath _path;
    private GameObject _target;
    private State _state;
    private float _updateTimer = 0.1f;
    private float _currentTime;

    //DATA MODIF
    private float attackDistance = 10;
    private float boostDistance = 30;

    void Start()
    {
        _players = BattleManager.getPlayers();
        _path = new NavMeshPath();
        _state = State.Chasing;
        _currentTime = 0.0f;
    }

    void FixedUpdate()
    {
        if (_currentTime >= _updateTimer)
        {
            if (!(GetComponent<NewRoombaController>().getCurrentState() is StunnedState))
            {
                LookNearest();
                if (_target != null)
                {
                    if (CalculatePathLength(_target.transform.position) <= attackDistance)
                    {
                        _state = State.Boosting;
                    }
                    if (CalculateCornerToCornerDistance(0, 1) >= boostDistance && CalculatePathLength(_target.transform.position) >= boostDistance)
                    {
                        _state = State.Boosting;
                    }
                    if (!(GetComponent<PowerUpManager>()._currentPower is NoPowerUp))
                    {
                        _state = State.UsingPower;
                    }
                    //Debug.Log(gameObject.name + " is " + _state.ToString());
                    switch (_state)
                    {
                        case State.Chasing:
                            Chase();
                            break;
                        case State.UsingPower:
                            UsingPowerUp();
                            break;
                        case State.Boosting:
                            Boost();
                            break;
                        case State.PickingPowerUp:
                            PickPower();
                            break;
                    }
                    _currentTime = 0.0f;
                }
            }
        }
        else
        {
            _currentTime += Time.deltaTime;
        }

        for (int i = 0; i < _path.corners.Length - 1; i++)
        {
            Debug.DrawLine(_path.corners[i], _path.corners[i + 1], Color.red);
        }
    }

    private void UsingPowerUp()
    {
        GetComponent<NewRoombaController>().updateAction(true);
    }

    private void PickPower()
    {
        bool b = NavMesh.CalculatePath(transform.position, _target.transform.position, NavMesh.AllAreas, _path);

        if (b && _path.corners.Length >= 2)
        {
            Vector2 waypoint = new Vector2(_path.corners[1].x, _path.corners[1].z);
            Vector2 roombaPos = new Vector2(transform.position.x, transform.position.z);
            Vector2 direction = waypoint - roombaPos;
            GetComponent<NewRoombaController>().updateMovement(direction.normalized);
        }
    }

    private void Boost()
    {
        GetComponent<NewRoombaController>().updateBoost(true);
    }

    private void Chase()
    {
        /**bool b = false;
        do
        {
            
            Debug.Log(b);
        } while (!b || _path.corners.Length < 2);*/

        Vector3 thisV2 = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 targetV2 = new Vector3(_target.transform.position.x, 0, _target.transform.position.z);
        bool b = NavMesh.CalculatePath(thisV2, targetV2, NavMesh.AllAreas, _path);
        //Debug.Log(gameObject.transform.parent.name + ": " + transform.position + " " + _target.transform.position);
        if (b && _path.corners.Length >= 2)
        {
            Vector2 waypoint = new Vector2(_path.corners[1].x, _path.corners[1].z);
            Vector2 roombaPos = new Vector2(transform.position.x, transform.position.z);
            Vector2 direction = waypoint - roombaPos;
            GetComponent<NewRoombaController>().updateMovement(direction.normalized);
        }
    }

    private void LookNearest()
    {

        Tuple<GameObject, float> nearRoombas = FindNearestRoomba();
        //Debug.Log(nearRoombas.Item1.ToString() + " " + nearRoombas.Item2);
        //Debug.Log("RoombaDone");
        //_target = nearRoombas.Item1;
        //_state = State.Chasing;
        Tuple<GameObject, float> nearPowers = FindNearestPower();
        //Tuple<GameObject, float> nearPowers = new Tuple<GameObject, float>(gameObject, 1000f);

        if (nearPowers.Item1.Equals(gameObject) || !(GetComponent<PowerUpManager>()._currentPower is NoPowerUp))
        {
            _target = nearRoombas.Item1;
            _state = State.Chasing;
        }
        else
        {
            if (nearRoombas.Item2 > nearPowers.Item2)
            {
                _target = nearPowers.Item1;
                _state = State.PickingPowerUp;
                //_target = nearRoombas.Item1;
                //_state = State.Chasing;
            }
            else
            {
                _target = nearRoombas.Item1;
                _state = State.Chasing;
            }
        }

    }
    private Tuple<GameObject, float> FindNearestPower()
    {
        float shortestDistance = 0;
        GameObject nearestPower = null;
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        if (powerUps.Length > 0)
        {
            foreach (GameObject powerUp in powerUps)
            {
                //float dist = CalculatePathLength(powerUp.transform.position);
                Vector2 thisV2 = new Vector2(transform.position.x, transform.position.z);
                Vector2 targetV2 = new Vector2(powerUp.transform.position.x, powerUp.transform.position.z);
                float dist = Vector2.Distance(thisV2, targetV2);
                if (shortestDistance == 0 || dist < shortestDistance)
                {
                    shortestDistance = dist;
                    nearestPower = powerUp;
                }
            }
        }

        if (shortestDistance != 0)
        {
            return new Tuple<GameObject, float>(nearestPower, shortestDistance);
        }
        else
        {
            return new Tuple<GameObject, float>(gameObject, shortestDistance);
        }
    }

    private Tuple<GameObject, float> FindNearestRoomba()
    {
        float shortestDistance = 0;
        GameObject nearestPlayer = null;
        foreach (GameObject player in _players)
        {
            if (!gameObject.Equals(player))
            {
                //float dist = CalculatePathLength(player.transform.position);
                Vector2 thisV2 = new Vector2(transform.position.x, transform.position.z);
                Vector2 targetV2 = new Vector2(player.transform.position.x, player.transform.position.z);
                float dist = Vector2.Distance(thisV2, targetV2);
                if (shortestDistance == 0 || dist < shortestDistance)
                {
                    shortestDistance = dist;
                    //Vector3.Distance(transform.position, player.transform.position);
                    nearestPlayer = player;
                }
            }
        }
        if (shortestDistance != 0)
        {
            GameObject ballons = nearestPlayer.transform.Find("Balloons").gameObject;
            int nBallons = ballons.transform.childCount;

            return new Tuple<GameObject, float>(ballons.transform.GetChild(0).gameObject, shortestDistance);
        }
        else
        {
            return new Tuple<GameObject, float>(gameObject, shortestDistance);
        }

    }

    private float CalculatePathLength(Vector3 targetPosition)
    {
        // Create a path and set it based on a target position.
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path);

        // Create an array of points which is the length of the number of corners in the path + 2.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        // The first point is the enemy's position.
        allWayPoints[0] = transform.position;

        // The last point is the target position.
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        // The points inbetween are the corners of the path.
        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        // Create a float to store the path length that is by default 0.
        float pathLength = 0;

        // Increment the path length by an amount equal to the distance between each waypoint and the next.
        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }

    private float CalculateCornerToCornerDistance(int from, int destination)
    {
        if (_path != null && _path.corners.Length >= destination + 1)
        {
            Vector2 fromPosition = new Vector2(_path.corners[from].x, _path.corners[from].z);
            Vector2 destinationPosition = new Vector2(_path.corners[destination].x, _path.corners[destination].z);

            return Vector2.Distance(fromPosition, destinationPosition);
        }
        else
        {
            return 0.0f;
        }
    }
}
