using System.Collections;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private NewRoombaController _controller;
    private float _invTime;
    private float _currTime;
    private PlayerVariables _pVar;

    public delegate void HitAction(GameObject obj);
    public static event HitAction OnHit;
    private void Awake()
    {
        _controller = GetComponent<NewRoombaController>();
        _pVar = gameObject.GetComponent<PlayerVariables>();
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
        Debug.Log(hisCollider.gameObject.name);
        Debug.Log(myCollider.gameObject.name);
        if (hisCollider.CompareTag("Wall"))
        {
            InteractionManager wallInteraction = collision.gameObject.GetComponent<InteractionManager>();
            if (wallInteraction != null)
            {
                wallInteraction.getInteraction().runInteraction(gameObject);
            }
        } else if (myCollider.CompareTag("Balloon") && (hisCollider.CompareTag("Knife") || hisCollider.CompareTag("Damaging")))
        {
            if (_currTime <= 0)
            {
                Destroy(myCollider.gameObject);
                _controller.GetHit();
                OnHit(gameObject);
                _currTime = _invTime;
            }
        } else if (myCollider.transform.parent.CompareTag("Player") && hisCollider.CompareTag("Damaging"))
        {
            _controller.GetStunned(2, (Random.value - 0.5f) * 10);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("KillArea"))
        {
            _pVar.insideRing = true;
            _pVar.timeForDead = 3;
        }
        else if(collision.name.Equals("Oil"))
        {
            Destroy(collision.gameObject);
            GetComponent<NewRoombaController>().GetStunned(2f, (Random.value - 0.5f));
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("KillArea"))
        {
            _pVar.insideRing = false;
        }
    }
}
