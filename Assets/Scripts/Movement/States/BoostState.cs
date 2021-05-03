using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostState : RoombaState
{
    private float _boostTime = 0.1f;
    private float _boostMaxSpeed = 300f;
    private float _currentTime = 0f;
    private Vector2 _direction;

    public BoostState()
    {

    }

    public void EnterState(NewRoombaController controller)
    {
        controller._boost = false;
        controller._phy.MaxSpeed = _boostMaxSpeed;
        controller._phy.ResetVelocity();
        _currentTime = _boostTime;
        _direction = controller._movement;
    }

    public Vector2 Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Stay(NewRoombaController controller)
    {
        Vector2 forward = new Vector2(controller.transform.forward.x,controller.transform.forward.z);
        controller._phy.addTorque(CustomPhysics.bisector(controller._movement, forward) * 30);
        controller._phy.addForce(_direction, 300);
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            controller._currentState = controller._normalState;
        }
    }
}
