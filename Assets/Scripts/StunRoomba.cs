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
}
