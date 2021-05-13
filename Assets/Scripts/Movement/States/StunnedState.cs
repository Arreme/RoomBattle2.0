using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : RoombaState
{

    private float _seconds;
    private float _rotation;
    private Rigidbody _rb;

    public StunnedState(float seconds, float rotation)
    {
        _seconds = seconds;
        _rotation = rotation;

    }

    public void EnterState(NewRoombaController controller)
    {
        _rb = controller.gameObject.GetComponent<Rigidbody>();
    }

    public void Stay(NewRoombaController controller)
    {
        _seconds -= Time.deltaTime;
        _rb.angularVelocity = new Vector3(0,10*_rotation,0);
        if (_seconds <= 0)
        {
            controller.onChangeState(controller._normalState);
        }
    }
}
