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
        Vector2 forward = new Vector2(controller.transform.forward.x, controller.transform.forward.z);
        controller._phy.addTorque(CustomPhysics.bisector(controller._movement, forward) * _rotation);
        _seconds -= Time.deltaTime;
        if (_seconds <= 0)
        {
            controller._currentState = controller._normalState;
        }
    }
}
