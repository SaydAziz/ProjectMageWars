using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject impact;

    float travelSpeed, deathTimer, damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.detectCollisions = false;
    }

    public void SetData(float TravelSpeed, float DeathTimer, float Damage)
    {
        travelSpeed = TravelSpeed;
        deathTimer = DeathTimer;
        damage = Damage;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(impact, transform).transform.parent = null;
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }

    public void Shoot()
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
