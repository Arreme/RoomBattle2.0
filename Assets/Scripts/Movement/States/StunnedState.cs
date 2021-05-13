using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : RoombaState
{

    private float _seconds;
    private float _rotation;
    private Rigidbody _rb;
    private float _force;

    public StunnedState(float seconds, float rotation,float force)
    {
        _seconds = seconds;
        _rotation = rotation;
        _force = force;

    }

    public void EnterState(NewRoombaController controller)
    {
        _rb = controller.gameObject.GetComponent<Rigidbody>();
        controller._phy.addForce(new Vector2(controller.transform.forward.x, controller.transform.forward.z), _force);
    }

    public void Stay(NewRoombaController controller)
    {
        _seconds -= Time.deltaTime;
        _rb.angularVelocity = new Vector3(0,15*_rotation,0);
        if (_seconds <= 0)
        {
            controller.onChangeState(controller._normalState);
        }
    }
}
