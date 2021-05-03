using UnityEngine;

public class PlayerVariables : MonoBehaviour
{
    [Header("Normal State")]
    public float _normalSpeed = 11f;
    public float _rotateSpeed = 30f;

    [Header("Boost State")]
    public float _boostTime = 0.1f;
    public float _boostMaxSpeed = 300f;
    public float _boostForce = 600f;

    [Header("Physics State")]
    public float _outMaxSpeed = 11f;
    public float _baseMaxSpeed = 11f;
    public float MaxSpeed
    {
        get { return _outMaxSpeed; }
        set
        {
            lerp = 0;
            _outMaxSpeed = value;
        }
    }
    public float rate = 0.8f;
    public float lerp = 1;
    public float lDrag = 1.14f;
    public float angDrag = 5f;
}
