using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    public Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.detectCollisions = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    public void Shoot(float travelSpeed, float deathTimer)
    {
        FunctionalUtility.SetLayerRecursively(this.gameObject, 0);
        rb.detectCollisions = true;
        transform.parent = null;
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = transform.forward * travelSpeed;
        Invoke("Die", deathTimer);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }


}
