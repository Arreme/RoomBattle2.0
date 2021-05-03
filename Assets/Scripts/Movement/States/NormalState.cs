using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : RoombaState
{
    private PlayerVariables _pVar;
    public NormalState(PlayerVariables _pVar)
    {
        this._pVar = _pVar;
    }

    public void EnterState(NewRoombaController controller)
    {
        
    }

    public void Stay(NewRoombaController controller)
    {
        Vector2 forward = new Vector2(controller.transform.forward.x, controller.transform.forward.z);
        controller._phy.addForce(controller._movement, _pVar._normalSpeed);
        controller._phy.addTorque(CustomPhysics.bisector(controller._movement, forward) * _pVar._rotateSpeed);
        if (controller._boost)
        {
            controller._currentState = controller._boostState;
            controller._boostState.EnterState(controller);
        }
    }
}
