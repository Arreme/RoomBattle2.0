public class NormalState : RoombaState
{
    private PlayerVariables _pVar;
    private PowerUpManager _pMan;
    public NormalState(PlayerVariables _pVar)
    {
        this._pVar = _pVar;
        _pMan = _pVar.GetComponent<PowerUpManager>();
    }

    public void EnterState(NewRoombaController controller)
    {
        
    }

    public void Stay(NewRoombaController controller)
    {
        controller._phy.addForce(controller._movement, _pVar._normalSpeed);
        controller._phy.addTorque(controller._movement * _pVar._rotateSpeed);
        if (controller._boost)
        {
            controller._currentState = controller._boostState;
            controller._boostState.EnterState(controller);
        } else if (controller._action) {
            _pMan.runPowerUp();
        }
    }
}
