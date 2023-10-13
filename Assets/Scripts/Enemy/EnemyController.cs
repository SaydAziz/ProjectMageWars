using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;

    Vector3 destPoint;
    bool hasDest;
    float roamRange;
    float range = 30;

    public float health { get; set; }

    private void Awake()
    {
        health = 100;
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Die();
        }
        Roam();
    }

    void Roam()
    {
        if (!hasDest)
        {
            SearchDest();
        }
        else if (hasDest)
        {
            agent.SetDestination(destPoint);
        }
        if (Vector3.Distance(transform.position, destPoint) < 10) hasDest = false;

    }

    void SearchDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);

        if (Physics.Raycast(destPoint, Vector3.down, groundLayer))
        {
            hasDest = true;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
