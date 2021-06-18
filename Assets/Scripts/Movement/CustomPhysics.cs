using UnityEngine;
using System.Collections;

/*
 * Determina la següent posició del Roomba o d'un objecte amb una massa
 * determinada
 */
public class CustomPhysics : MonoBehaviour
{
    private Rigidbody _rb;

    public bool dead;

    private Vector2 _force = Vector2.zero;
    private Vector2 _angularForce = Vector2.zero;

    PlayerVariables _pVar;

    private void Start()
    {
        _pVar = gameObject.GetComponent<PlayerVariables>();
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.position = transform.position;
        _rb.rotation = transform.rotation;
        _rb.centerOfMass = Vector3.zero;
        dead = false;
        StartCoroutine(GetComponent<RoombaVFX>().checkForRunVFX(_rb));
    }

    public void FixedUpdate()
    {
        _rb.drag = _pVar.lDrag;
        _rb.angularDrag = _pVar.angDrag;
        _pVar.lerp += _pVar.lerp >= 1 ? 0 : _pVar.rate * Time.deltaTime;
        LinearMovement();
        AngularMovement();
    }

    #region(AngularMovement)
    private void AngularMovement()
    {
        if (Vector2.zero != _angularForce)
        {
            Vector2 forward = new Vector2(transform.forward.x, transform.forward.z);
            Vector3 bisec = bisector(_angularForce.normalized, forward);
            _rb.angularVelocity = new Vector3(0, _angularForce.magnitude * Mathf.Sin(-Mathf.Deg2Rad * Vector2.SignedAngle(new Vector2(bisec.x, bisec.z), _angularForce.normalized)) / _rb.mass, 0);
            _angularForce = Vector3.zero;
        }
    }

    public void addTorque(Vector2 force)
    {
        _angularForce = force;
    }
    #endregion

    #region(LinearVelocity)

    private void LinearMovement()
    {
        Vector3 a = new Vector3(_force.x, 0, _force.y) / _rb.mass;
        _force = Vector2.zero;
        _rb.velocity += finalVelocity(a);
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _pVar._outMaxSpeed);
        _pVar._outMaxSpeed = Mathf.Lerp(_pVar._outMaxSpeed, _pVar._baseMaxSpeed, _pVar.lerp);
    }

    public void addForce(Vector2 direction, float newtons)
    {
        _force = direction * newtons;
    }

    public Vector3 finalVelocity(Vector3 a)
    {
        return a * Time.deltaTime;
    }
    #endregion

    public void LateUpdate()
    {
        if (!dead)
        {
            _rb.angularVelocity = new Vector3(0, _rb.angularVelocity.y, 0);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
        else
        {
            _rb.constraints = 0;
            _rb.AddForce((transform.forward * 3 + new Vector3(0, 3, 0)) * 100, ForceMode.Acceleration);
            _rb.AddTorque(transform.forward * 10);
        }

    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        Vector3 myCollisionNormal = collisionInfo.contacts[0].normal;

    }

    public void ResetVelocity()
    {
        _rb.velocity = Vector3.zero;
    }

    public static Vector3 bisector(Vector2 a, Vector2 b)
    {
        if (Vector2.Dot(a, b) <= -0.9999f)
        {
            return new Vector3(a.y, 0, -a.x);
        }
        Vector2 bisector = (b.magnitude * a + a.magnitude * b).normalized;
        return new Vector3(bisector.x, 0, bisector.y);
    }
}
