using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : RoombaState
{
    private float _normalSpeed = 11f;
    private float _rotateSpeed = 30f;

    public NormalState()
    {
        
    }

    public void EnterState(NewRoombaController controller)
    {
        
    }

    public Vector2 Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Stay(NewRoombaController controller)
    {
        Vector2 forward = new Vector2(controller.transform.forward.x, controller.transform.forward.z);
        controller._phy.addForce(controller._movement, _normalSpeed);
        controller._phy.addTorque(CustomPhysics.bisector(controller._movement, forward) * _rotateSpeed);
        if (controller._boost)
        {
            controller._currentState = controller._boostState;
            controller._boostState.EnterState(controller);
        }
    }
}
