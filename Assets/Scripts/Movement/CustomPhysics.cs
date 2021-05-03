using UnityEngine;

/*
 * Determina la següent posició del Roomba o d'un objecte amb una massa
 * determinada
 */
public class CustomPhysics : MonoBehaviour
{
    private Rigidbody _rb;

    private Vector2 _force = Vector2.zero;
    private Vector2 _input;
    private float _outMaxSpeed = 11f;
    private float _baseMaxSpeed = 11f;
    private float rate;
    private Vector2 _angularForce = Vector2.zero;

    public float MaxSpeed
    {
        get { return _baseMaxSpeed; }
        set
        {
            rate = 0;
            _outMaxSpeed = value < 0 ? 0 : value;
        }
    }

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.position = transform.position;
        _rb.rotation = transform.rotation;
        rate = 1;
        //_rb.centerOfMass = Vector2.zero;
    }

    public void FixedUpdate()
    {
        rate += rate >= 1 ? 0 : 0.8f * Time.deltaTime;
        LinearMovement();
        AngularMovement();
    }

    #region(AngularMovement)
    private void AngularMovement()
    {
        if (Vector2.zero != _angularForce)
        {
            _rb.angularVelocity = new Vector3(0, _angularForce.magnitude* Mathf.Sin(-Mathf.Deg2Rad * Vector2.SignedAngle(_angularForce.normalized, _input)) / _rb.mass, 0);
            _angularForce = Vector3.zero;
        }
    }

    public void addTorque(Vector3 force)
    {
        _angularForce = new Vector2(force.x,force.z);
    }

    public void addInput(Vector2 input)
    {
        _input = input;
    }
    #endregion

    #region(LinearVelocity)

    private void LinearMovement()
    {
        Vector3 a = new Vector3(_force.x,0,_force.y) / _rb.mass;
        _force = Vector2.zero;
        _rb.velocity += finalVelocity(a);
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _outMaxSpeed);
        _outMaxSpeed = Mathf.Lerp(_outMaxSpeed,_baseMaxSpeed,rate);
    }

    public void addForce(Vector2 direction,float newtons)
    {
        _force = direction * newtons;
    }

    public Vector3 finalVelocity(Vector3 a)
    {
        //vel = v0 + a*t
        return a*Time.deltaTime;
    }
    #endregion

    public void LateUpdate()
    {
        _rb.angularVelocity = new Vector3(0, _rb.angularVelocity.y, 0);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
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
        //dot( A, A )*dot( B, B ) - dot( A, B ) * dot( A, B ) = 0
        if (Vector2.Dot(a,b) <= -0.9999f)
        {
            return new Vector3(a.y,0, - a.x);
        }
        Vector2 bisector = (b.magnitude * a + a.magnitude * b).normalized;
        return new Vector3(bisector.x,0,bisector.y);
    }
}
