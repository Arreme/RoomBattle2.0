using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRoombaController : MonoBehaviour
{
    public Vector2 _movement;
    public bool _action;
    public bool _boost;
    public int balloons = 3;

    public CustomPhysics _phy;

    //States
    public RoombaState _currentState;

    public NormalState _normalState;
    public BoostState _boostState;

    void Start()
    {
        _phy = GetComponent<CustomPhysics>();
        _normalState = new NormalState();
        _boostState = new BoostState();

        _currentState = _normalState;
    }

    
    void Update()
    {
        _currentState.Stay(this);
        if (balloons == 0)
        {
            Destroy(gameObject);
        }
    }

    public void GetHit()
    {
        balloons -= 1;
        //TriggerInvincibility
    }

    #region(InputManagement)
    public void updateMovement(Vector2 movement)
    {
        _movement = movement;
        _phy.addInput(movement);
    }

    public void updateBoost(bool boost)
    {
        if (boost && !_boost)
        {
            _boost = boost;
        }
    }

    public void updateAction(bool action)
    {
        _action = action;
    }
    #endregion
}
