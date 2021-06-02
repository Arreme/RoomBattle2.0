using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private NewRoombaController _controller;
    private float _invTime;
    private bool _isInvincible = false;
    private PlayerVariables _pVar;
    [SerializeField] private List<GameObject> models;
    private void Awake()
    {
        _controller = GetComponent<NewRoombaController>();
        _pVar = gameObject.GetComponent<PlayerVariables>();
        _invTime = 2f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider myCollider = collision.contacts[0].thisCollider;
        Collider hisCollider = collision.contacts[0].otherCollider;
        if (hisCollider.CompareTag("Wall"))
        {   
            InteractionManager wallInteraction = collision.gameObject.GetComponent<InteractionManager>();
            if (wallInteraction != null)
            {
                wallInteraction.getInteraction().runInteraction(gameObject);
            }
        }
        else if (myCollider.CompareTag("Balloon") && (hisCollider.CompareTag("Knife") || hisCollider.CompareTag("Damaging")))
        {
            if (!_isInvincible)
            {
                if (PlayerConfigManager.Instance._teamsEnabled)
                {
                    InputManager myInput = GetComponent<InputManager>();
                    InputManager hisInput = hisCollider.GetComponentInParent<InputManager>();
                    Debug.Log(hisInput._teamBlue);
                    Debug.Log(myInput._teamBlue);
                    if (hisInput != null && myInput._teamBlue != hisInput._teamBlue)
                    {
                        TakeDamage(myCollider);
                    }
                } else
                {
                    TakeDamage(myCollider);
                }
            }
        }
        else if (hisCollider.CompareTag("Balloon") && myCollider.CompareTag("Knife"))
        {

        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb.velocity.magnitude >= 12)
            {
                Vector3 direction = transform.position - collision.transform.position;
                _controller.GetStunned(0.5f,new Vector2(direction.x,direction.z),400);
            }
        }
        else if (myCollider.transform.parent.CompareTag("Player") && hisCollider.CompareTag("Damaging"))
        {
            Vector3 direction = Vector3.Normalize(hisCollider.transform.position - myCollider.transform.position);
            _controller.GetStunned(1.5f, new Vector2(-direction.x,-direction.z) ,2000);
        }
    }

    private void TakeDamage(Collider myCollider)
    {
        Destroy(myCollider.gameObject);
        _controller.GetHit();
        BattleManager.Instance.Explosion(gameObject);
        StartCoroutine(invincible());
    }

    private IEnumerator invincible()
    {
        _isInvincible = true;
        bool _zero = true;
        for (float i = 0; i < _invTime; i += 0.1f)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            if (_zero)
            {
                foreach(GameObject model in models)
                {
                    model.SetActive(false);
                }
                _zero = false;
            }
            else
            {
                foreach (GameObject model in models)
                {
                    model.SetActive(true);
                }
                _zero = true;
            }
            yield return new WaitForSeconds(0.1f);
        }

        if (!_zero)
        {
            foreach (GameObject model in models)
            {
                model.SetActive(true);
            }
        }
        _isInvincible = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("KillArea"))
        {
            _pVar.insideRing = true;
            _pVar.timeForDead = 3;
        }
        else if (collision.CompareTag("Oil"))
        {
            Destroy(collision.gameObject);
            GetComponent<NewRoombaController>().GetStunned(2f, new Vector2(transform.forward.x,transform.forward.z), 200);
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
