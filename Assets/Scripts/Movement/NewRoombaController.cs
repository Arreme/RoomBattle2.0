using UnityEngine;

public class NewRoombaController : MonoBehaviour
{
    public Vector2 _movement;
    public bool _action;
    public bool _boost;
    public int balloons = 3;

    public CustomPhysics _phy;
    public PlayerVariables _pVar;
    //States
    public RoombaState _currentState;

    public NormalState _normalState;
    public BoostState _boostState;

    void Start()
    {
        _pVar = GetComponent<PlayerVariables>();
        _phy = GetComponent<CustomPhysics>();
        _normalState = new NormalState(_pVar);
        _boostState = new BoostState(_pVar);

        _currentState = _normalState;
    }

    
    void FixedUpdate()
    {
        _currentState.Stay(this);
        if (balloons == 0)
        {
            Destroy(gameObject);
        }
    }

    public void GetHit()
    {
        balloons -= 1;
        GetStunned(1, Random.value*10);
        _pVar.MaxSpeed = 1000;
        _phy.addForce(new Vector2(transform.forward.x,transform.forward.z), 5000);
    }

    internal void GetStunned(float v, float value)
    {
        _currentState = new StunnedState(v,value);
    }

    #region(InputManagement)
    public void updateMovement(Vector2 movement)
    {
        _movement = movement;
        _phy.addInput(movement);
    }

    public void updateBoost(bool boost)
    {
        if (boost && !_boost)
        {
            _boost = boost;
        }
    }

    public void updateAction(bool action)
    {
        _action = action;
    }
    #endregion
}
