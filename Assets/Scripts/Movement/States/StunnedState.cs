using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : RoombaState
{

    private float _seconds;
    private float _rotation;
    private Vector2 _movement;

    public StunnedState(float seconds, float rotation)
    {
        _seconds = seconds;
        _rotation = rotation;
    }

    public void EnterState(NewRoombaController controller)
    {
        _movement = controller._movement;
    }

    public void Stay(NewRoombaController controller)
    {
        controller._phy.addTorque(controller._movement * _rotation);
        _seconds -= Time.deltaTime;
        if (_seconds <= 0)
        {
            controller._currentState = controller._normalState;
        }
    }
}
