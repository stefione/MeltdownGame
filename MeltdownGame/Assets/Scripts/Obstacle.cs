using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float _hitStrength;
    private float _velocity;
    private Vector3 _prevPos;

    // Update is called once per frame
    void Update()
    {
        _velocity = (transform.position - _prevPos).magnitude;
        _prevPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            collision.transform.root.GetComponent<CharacterController>().ActivateRagdoll();
            Rigidbody collisionRb = collision.gameObject.GetComponent<Rigidbody>();
            if (collisionRb != null)
            {
                Vector3 dir = collision.gameObject.transform.position - collision.GetContact(0).point;
                collisionRb.AddForce(_velocity * _hitStrength * dir, ForceMode.Impulse);
            }
        }
    }
}
