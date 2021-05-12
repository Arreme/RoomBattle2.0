using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBolaController : MonoBehaviour
{
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _rb.velocity = transform.forward * 25;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider myCollider = collision.contacts[0].thisCollider;
        Collider hisCollider = collision.contacts[0].otherCollider;
        if (hisCollider.transform.parent.CompareTag("Player"))
        {
            Destroy(gameObject);
        } else
        {
            Vector3 normal = collision.contacts[0].normal;
            float dotProduct = Vector2.Dot(new Vector2(transform.forward.x, transform.forward.z), new Vector2(normal.x, normal.z));
            transform.forward = transform.forward - 2 * dotProduct * normal;
        }
    }
}
