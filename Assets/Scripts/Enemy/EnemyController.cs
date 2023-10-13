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
    [SerializeField] Transform spellSpawn;
    [SerializeField] Spell spell;

    bool noAttackCD = true;
    float attackCD = 2f;

    Vector3 destPoint;
    bool hasDest;
    float roamRange = 30;
    float sightRange = 15;
    float attackRange = 10;
    bool seesPlayer, canAttack;
    public float health { get; set; }

    private void Awake()
    {
        health = 100;
    }

    private void FixedUpdate()
    {
        seesPlayer = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        canAttack = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if (health <= 0) Die();

        if (seesPlayer && canAttack)
        {
            agent.SetDestination(transform.position);
            transform.LookAt(player.transform.position);
            Attack();
        }
        else if (seesPlayer)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            Roam();
        }
    }

    void Attack()
    {
        if (noAttackCD)
        {
            noAttackCD = false;
            Debug.Log("BANG BANG");
            spell.Queue(spellSpawn.transform);
            spell.Use();
            Invoke("resetAttack", attackCD);
        }     
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
        float z = Random.Range(-roamRange, roamRange);
        float x = Random.Range(-roamRange, roamRange);

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

    void resetAttack()
    {
        noAttackCD = true;
    }
}
