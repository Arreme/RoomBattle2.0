using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunRoomba : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<NewRoombaController>().GetStunned(2,new Vector2(-collision.gameObject.transform.forward.x, -collision.gameObject.transform.forward.z),1200);
        }
    }
}
