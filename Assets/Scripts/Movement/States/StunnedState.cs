using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : RoombaState
{

    private float _seconds;
    private float _rotation;

    public StunnedState(float seconds, float rotation)
    {
        _seconds = seconds;
        _rotation = rotation;

    }

    public void EnterState(NewRoombaController controller)
    {
        Vector2 _movement = new Vector2(controller.transform.forward.x,controller.transform.forward.z);
        _movement = Vector2.Perpendicular(_movement);
        controller._phy.addTorque(_movement * _rotation * 200);
    }

    public void Stay(NewRoombaController controller)
    {
        _seconds -= Time.deltaTime;
        if (_seconds <= 0)
        {
            controller.onChangeState(controller._normalState);
        }
    }
}
