using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunRoomba : MonoBehaviour
{
    [SerializeField]
    private float force = 2000;
    [SerializeField]
    private float seconds = 2;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<NewRoombaController>().GetStunned(seconds,new Vector2(collision.transform.position.x-transform.position.x, collision.transform.position.z - transform.position.z),force);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<NewRoombaController>().GetStunned(seconds, new Vector2(collision.transform.position.x - transform.position.x, collision.transform.position.z - transform.position.z), force);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject parent = other.transform.parent.gameObject;
        if (parent.CompareTag("Player"))
        {
            if (parent.GetComponent<NewRoombaController>()?.getCurrentState().GetType() != typeof(StunnedState) ) parent.GetComponent<NewRoombaController>()?.GetStunned(seconds, new Vector2(parent.transform.position.x - transform.position.x, parent.transform.position.z - transform.position.z), force);
        }
    }
}
