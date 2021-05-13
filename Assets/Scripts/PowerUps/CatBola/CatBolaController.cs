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

    private void LateUpdate()
    {
        _rb.velocity = transform.forward * 25;
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider hisCollider = collision.contacts[0].otherCollider;
        Transform parent = hisCollider.transform.parent;
        if (parent != null && parent.CompareTag("Player"))
        {
            Destroy(gameObject);
        } else
        {
            Vector3 normal = collision.contacts[0].normal;
            float dotProduct = Vector2.Dot(new Vector2(transform.forward.x, transform.forward.z), new Vector2(normal.x, normal.z));
            transform.forward = transform.forward - 2 * dotProduct * new Vector3(normal.x,0,normal.z);
        }
    }
}
