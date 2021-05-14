using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunRoomba : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<NewRoombaController>().GetStunned(2,new Vector2(-transform.up.x, -transform.up.z),4000);
        }
    }
}
