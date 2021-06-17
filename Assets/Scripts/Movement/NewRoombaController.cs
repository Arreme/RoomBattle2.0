using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NewRoombaController : MonoBehaviour
{
    public Vector2 _movement;
    public bool _action;
    public bool _boost;
    public int balloons = 3;

    [SerializeField] private Collider _knife;
    [SerializeField] private GameObject _vEffect;

    public RoombaVFX _vfx;
    public CustomPhysics _phy;
    public PlayerVariables _pVar;
    //States
    private RoombaState _currentState;

    public NormalState _normalState;
    public BoostState _boostState;

    [SerializeField] private GameObject deathCanvas;
    private Quaternion _lockTransform;
    private Image _image;


    void Start()
    {
        _lockTransform = deathCanvas.transform.rotation;
        _pVar = GetComponent<PlayerVariables>();
        _phy = GetComponent<CustomPhysics>();
        _normalState = new NormalState(_pVar);
        _boostState = new BoostState(_pVar);
        _currentState = _normalState;
        _image = deathCanvas.GetComponentInChildren<Image>();
    }


    void FixedUpdate()
    {
        _currentState.Stay(this);
        if (balloons == 0 && !_phy.dead)
        {
            StartCoroutine(die());
        }

        if (!_pVar.insideRing)
        {
            _image.enabled = true;
            _pVar.currentTimeForDead -= Time.deltaTime;
            _image.fillAmount = _pVar.currentTimeForDead / _pVar.timeForDead;
            if (_pVar.currentTimeForDead <= 0 && !_phy.dead)
            {
                StartCoroutine(die());
            }
        }
        else
        {
            _image.enabled = false;
            _pVar.currentTimeForDead = _pVar.timeForDead;
        }

    }
    private void LateUpdate()
    {
        deathCanvas.transform.rotation = _lockTransform;
    }

    private IEnumerator die()
    {
        _phy.dead = true;
        _knife.enabled = false;
        BattleManager.Instance.removePlayers(gameObject);
        GetStunned(10, new Vector2(transform.forward.x, transform.forward.z), 700);
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void GetHit()
    {
        balloons -= 1;
        GetStunned(0.5f, new Vector2(transform.forward.x, transform.forward.z), 700);
        _pVar.MaxSpeed = 100;
        StressReceiver.InduceStress(30f);
        StartCoroutine(changeKnife());
    }

    public IEnumerator changeKnife()
    {
        _knife.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _knife.enabled = true;
    }

    public void onChangeState(RoombaState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void GetStunned(float v, Vector2 direction, float force)
    {
        _currentState = new StunnedState(v, direction, force);
        _currentState.EnterState(this);
        StartCoroutine(_vfx.activateStunned(v));
    }

    public RoombaState getCurrentState()
    {
        return _currentState;
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
