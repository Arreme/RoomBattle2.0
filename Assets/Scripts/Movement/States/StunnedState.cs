using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : RoombaState
{

    private float _seconds;
    private float _rotation;
    private Rigidbody _rb;
    private float _force;
    private Vector2 _direction;

    public StunnedState(float seconds, Vector2 direction ,float force)
    {
        _seconds = seconds;
        _rotation = Random.Range(0, 2) * 2 - 1;
        _force = force;
        _direction = direction;

    }

    public void EnterState(NewRoombaController controller)
    {
        _rb = controller.gameObject.GetComponent<Rigidbody>();
        controller._phy.addForce(new Vector2(_direction.x, _direction.y), _force);
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
