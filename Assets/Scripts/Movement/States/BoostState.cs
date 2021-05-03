using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostState : RoombaState
{
    private float _currentTime = 0f;
    private Vector2 _direction;

    private PlayerVariables _pVar;

    public BoostState(PlayerVariables _pVar)
    {
        this._pVar = _pVar;
    }

    public void EnterState(NewRoombaController controller)
    {
        controller._boost = false;
        _pVar.MaxSpeed = _pVar._boostMaxSpeed;
        controller._phy.ResetVelocity();
        _currentTime = _pVar._boostTime;
        _direction = controller._movement;
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
