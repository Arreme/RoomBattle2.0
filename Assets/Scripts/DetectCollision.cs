using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private NewRoombaController _controller;
    private float _invTime;
    private float _currTime;

    public delegate void HitAction(GameObject obj);
    public static event HitAction OnHit;
    private void Awake()
    {
        _controller = GetComponent<NewRoombaController>();
        _invTime = 2f;
        _currTime = 0;
    }

    private void FixedUpdate()
    {
        _currTime -= Time.deltaTime;
        _currTime = _currTime <= 0 ? 0 : _currTime;
    }



    private void OnCollisionEnter(Collision collision)
    {
        Collider myCollider = collision.contacts[0].thisCollider;
        Collider hisCollider = collision.contacts[0].otherCollider;
        if (hisCollider.CompareTag("Wall"))
        {
            Debug.Log("Hit a wall");
        } else if (myCollider.CompareTag("Balloon") && hisCollider.CompareTag("Knife"))
        {
            if (_currTime <= 0)
            {
                Destroy(myCollider.gameObject);
                _controller.GetHit();
                OnHit(gameObject);
                _currTime = _invTime;
            }
        } else if (myCollider.CompareTag("Body") && hisCollider.CompareTag("Knife"))
        {
            Debug.Log("I go away");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        PlayerVariables _pVar = gameObject.GetComponent<PlayerVariables>();
        _pVar.insideRing = true;
        _pVar.timeForDead = 3;

    }

    private void OnTriggerExit(Collider other)
    {
        PlayerVariables _pVar = gameObject.GetComponent<PlayerVariables>();
        _pVar.insideRing = false;
    }
}
