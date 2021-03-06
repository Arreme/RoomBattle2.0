using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostState : RoombaState
{
    private float _currentTime = 0f;
    private Vector2 _direction;

    private PlayerVariables _pVar;
    private PowerUpManager _pMan;

    public BoostState(PlayerVariables _pVar)
    {
        this._pVar = _pVar;
        _pMan = _pVar.GetComponent<PowerUpManager>();
    }

    public void EnterState(NewRoombaController controller)
    {
        controller._boost = false;
        _pVar.MaxSpeed = _pVar._boostMaxSpeed;
        controller._phy.ResetVelocity();
        _currentTime = _pVar._boostTime;
        _direction = new Vector2(controller.transform.forward.x, controller.transform.forward.z);
        controller.gameObject.GetComponent<Animation>().Play();
        controller._vfx._boost.SendEvent("BoostPlay");
        AudioManager.Instance._PlaySFX("Dash");
    }

    public void Stay(NewRoombaController controller)
    {
        controller._phy.addForce(_direction, _pVar._boostForce);
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            controller.onChangeState(controller._normalState);
        }
        else if (controller._action)
        {
            _pMan.runPowerUp();
            controller._action = false;
        }
    }
}
