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
        Fleeing,
    }

    private NavMeshPath _path;
    private GameObject _target;
    private State _state;
    private float _updateTimer = 0.4f;
    private float _currentTime;
    private Vector2 _direction;
    private Vector3 fleePosition;
    private float _boostTimer = 1.0f;
    private float _boostCurrentTime;

    //DATA MODIFICAR
    private float butcherDistance = 10;
    private float boostDistance = 20;
    private float PowerOrChaseMargin = 30;

    //TESTING
    private Vector3 point;

    void Start()
    {
        _path = new NavMeshPath();
        _state = State.Chasing;
        _currentTime = 0.0f;
        fleePosition = Vector3.zero;
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

                    bool checkButcher = CheckButcher();

                    /*&& Vector3.Distance(transform.position, _target.transform.position) >= butcherDistance
                    if (checkButcher && Vector3.Distance(transform.position, _target.transform.position) >= butcherDistance)
                    {
                        _state = State.Fleeing;
                    }*/
                    if (!(GetComponent<PowerUpManager>()._currentPower is NoPowerUp))
                    {
                        _state = State.UsingPower;
                    }
                    if (CalculateCornerToCornerDistance(0, 1) >= boostDistance && CalculatePathLength(_target.transform.position) >= boostDistance && _boostCurrentTime >= _boostTimer)
                    {
                        _state = State.Boosting;
                    }

                    /**else if (_butcher)
                    {
                        _state = State.Fleeing;
                    }
                    else if (!checkButcher)
                    {

                    }*/

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
                            _boostCurrentTime = 0.0f;
                            break;
                        case State.PickingPowerUp:
                            PickPower();
                            break;
                        case State.Fleeing:
                            Flee();
                            break;
                    }
                    _currentTime = 0.0f;
                }
            }
        }
        else
        {
            _currentTime += Time.deltaTime;
            _boostCurrentTime += Time.deltaTime;
        }

        //DEBUG
        /**
        for (int i = 0; i < _path.corners.Length - 1; i++)
        {
            Debug.DrawLine(_path.corners[i], _path.corners[i + 1], Color.red);
        }
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && this.enabled)
        {
            ClashingSolver();
        }
    }

    private int checkBalloons()
    {
        GameObject ballons = transform.Find("Balloons").gameObject;
        return ballons.transform.childCount;
    }

    private bool CheckButcher()
    {
        bool check = false;
        if (_target != null && !_target.gameObject.CompareTag("PowerUp"))
        {
            if (_target.CompareTag("Balloon"))
            {
                bool butcherCheck = _target.transform.parent.parent.Find("Butcher").gameObject.activeSelf;
                check = butcherCheck;
            }
            else if (_target.CompareTag("Player"))
            {
                bool butcherCheck = _target.transform.Find("Butcher").gameObject.activeSelf;
                check = butcherCheck;
            }
        }
        return check;
    }

    private bool CheckButcher(GameObject roomba)
    {
        bool check = false;
        if (roomba != null && !roomba.gameObject.CompareTag("PowerUp"))
        {
            bool butcherCheck = roomba.transform.Find("Butcher").gameObject.activeSelf;
            check = butcherCheck;
        }
        return check;
    }

    private void Flee()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _target.transform.position);
        bool butcherCheck = false;

        if (fleePosition == Vector3.zero || checkFleePosition(fleePosition))
        {
            do
            {
                fleePosition = GetRandomLocation();

                butcherCheck = checkFleePosition(fleePosition);
            }
            while (butcherCheck);

            point = fleePosition;
        }

        NavMesh.CalculatePath(transform.position, fleePosition, NavMesh.AllAreas, _path);

        if (_path.corners.Length >= 2)
        {
            Vector2 waypoint = new Vector2(_path.corners[1].x, _path.corners[1].z);
            Vector2 roombaPos = new Vector2(transform.position.x, transform.position.z);
            _direction = waypoint - roombaPos;
            GetComponent<NewRoombaController>().updateMovement(_direction.normalized);
        }
    }

    private bool checkFleePosition(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapBox(
               position,
               new Vector3(butcherDistance, 0, butcherDistance),
               Quaternion.identity);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.Equals(_target.transform.parent.parent))
            {
                return true;
            }
            i++;
        }
        return false;
    }

    public Vector3 GetRandomLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        int t = UnityEngine.Random.Range(0, navMeshData.indices.Length - 3);

        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], UnityEngine.Random.value);
        point = Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], UnityEngine.Random.value);

        return point;
    }

    private void ClashingSolver()
    {
        Vector2 perpendicular = Vector2.Perpendicular(_direction);
        int random = UnityEngine.Random.Range(1, 11);

        if (random % 2 == 0)
        {
            perpendicular = perpendicular * -1;
        }

        Vector2 newDirection = _direction + perpendicular;
        _direction = newDirection.normalized;

        GetComponent<NewRoombaController>().updateMovement(_direction);
        GetComponent<NewRoombaController>().updateBoost(true);
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
        Vector3 thisV2 = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 targetV2 = new Vector3(_target.transform.position.x, 0, _target.transform.position.z);
        bool b = NavMesh.CalculatePath(thisV2, targetV2, NavMesh.AllAreas, _path);
        if (b && _path.corners.Length >= 2)
        {
            Vector2 waypoint = new Vector2(_path.corners[1].x, _path.corners[1].z);
            Vector2 roombaPos = new Vector2(transform.position.x, transform.position.z);
            _direction = waypoint - roombaPos;
            GetComponent<NewRoombaController>().updateMovement(_direction.normalized);
        }
    }

    private void LookNearest()
    {
        Tuple<GameObject, float> nearRoombas = FindNearestRoomba();
        Tuple<GameObject, float> nearPowers = FindNearestPower();

        if (nearPowers.Item1.Equals(gameObject) || !(GetComponent<PowerUpManager>()._currentPower is NoPowerUp))
        {
            _target = nearRoombas.Item1;
            _state = State.Chasing;
        }
        else
        {
            int nBallons = checkBalloons();

            if (nBallons <= 1 && nearRoombas.Item2 - nearPowers.Item2 >= PowerOrChaseMargin)
            {
                _target = nearPowers.Item1;
                _state = State.PickingPowerUp;
            }
            else
            {
                if (nearRoombas.Item2 > nearPowers.Item2)
                {
                    _target = nearPowers.Item1;
                    _state = State.PickingPowerUp;
                }
                else
                {
                    _target = nearRoombas.Item1;
                    _state = State.Chasing;
                }
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
                float dist = CalculatePathLength(powerUp.transform.position);
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
        int countEnemies = 0;

        if (PlayerConfigManager.Instance._teamsEnabled)
        {
            foreach (GameObject player in BattleManager.getPlayers())
            {
                if (!player.GetComponent<CustomPhysics>().dead)
                {
                    if (gameObject.GetComponent<InputManager>()._teamBlue != player.GetComponent<InputManager>()._teamBlue)
                    {
                        countEnemies++;
                    }
                }
            }
        }

        foreach (GameObject player in BattleManager.getPlayers())
        {
            if (!gameObject.Equals(player) && !player.GetComponent<CustomPhysics>().dead)
            {
                if (PlayerConfigManager.Instance._teamsEnabled)
                {
                    if (gameObject.GetComponent<InputManager>()._teamBlue != player.GetComponent<InputManager>()._teamBlue)
                    {
                        float dist = CalculatePathLength(player.transform.position);
                        if (shortestDistance == 0 || dist < shortestDistance)
                        {
                            if (!CheckButcher(player) || countEnemies == 1)
                            {
                                shortestDistance = dist;
                                nearestPlayer = player;
                            }
                        }
                    }
                }
                else
                {
                    float dist = CalculatePathLength(player.transform.position);
                    if (shortestDistance == 0 || dist < shortestDistance)
                    {
                        if (!CheckButcher(player) || BattleManager.getPlayers().Count == 2)
                        {
                            shortestDistance = dist;
                            nearestPlayer = player;
                        }
                    }
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