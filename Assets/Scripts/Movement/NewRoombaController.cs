using System.Collections;
using UnityEngine;

public class NewRoombaController : MonoBehaviour
{
    public Vector2 _movement;
    public bool _action;
    public bool _boost;
    public int balloons = 3;

    [SerializeField] private Collider _knife;
    [SerializeField] private GameObject _vEffect;

    public CustomPhysics _phy;
    public PlayerVariables _pVar;
    //States
    private RoombaState _currentState;

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
            StartCoroutine(die());
        }

        if (!_pVar.insideRing) {
            _pVar.currentTimeForDead -= Time.deltaTime;
            if (_pVar.currentTimeForDead <= 0)
            {
                StartCoroutine(die());
            }
        } else
        {
            _pVar.currentTimeForDead = _pVar.timeForDead;
        }
    }

    private IEnumerator die()
    {
        _phy.dead = true;
        _knife.enabled = false;
        BattleManager.removePlayers(gameObject);
        GetStunned(10,700);
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void GetHit()
    {
        balloons -= 1;
        GetStunned(1f,700);
        _pVar.MaxSpeed = 100;
        StressReceiver.InduceStress(30f);
        StartCoroutine(changeKnife());
    }

    public IEnumerator changeKnife()
    {
        _knife.enabled = false;
        Debug.Log("Knife disabled");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Knife enabled");
        _knife.enabled = true;
    }

    public void onChangeState(RoombaState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void GetStunned(float v,float force)
    {
        _currentState = new StunnedState(v, Random.Range(0, 2) * 2 - 1, force);
        _currentState.EnterState(this);
        StartCoroutine(StunnedVFX(v));
    }

    private IEnumerator StunnedVFX(float seconds)
    {
        _vEffect.SetActive(true);
        yield return new WaitForSeconds(seconds);
        _vEffect.SetActive(false);
    }

    #region(InputManagement)
    public void updateMovement(Vector2 movement)
    {
        _movement = movement;   
    }

    public void updateBoost(bool boost)
    {
        _boost = boost;
    }

    public void updateAction(bool action)
    {
        _action = action;
    }
    #endregion
}
