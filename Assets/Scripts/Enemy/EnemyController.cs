using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public abstract class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected LayerMask groundLayer, playerLayer;
    [SerializeField] protected Transform spellSpawn;
    [SerializeField] protected Spell spell;

    protected bool noAttackCD = true;
    protected float attackCD = 2f;

    protected Vector3 destPoint;
    protected bool hasDest;
    protected float roamRange = 30;
    protected float sightRange = 15;
    protected float attackRange = 10;
    protected bool seesPlayer, canAttack;
    public float health { get; set; }

    protected virtual void Awake()
    {
        health = 100;
        player = GameObject.Find("Player");
        playerLayer = LayerMask.NameToLayer("Player");
    }

    protected virtual void FixedUpdate()
    {
        seesPlayer = Vector3.Distance(transform.position, player.transform.position) < sightRange;
            //Physics.CheckSphere(transform.position, sightRange, playerLayer);
        canAttack = Vector3.Distance(transform.position, player.transform.position) < attackRange;
        //Physics.CheckSphere(transform.position, attackRange, playerLayer);
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

    protected virtual void Attack()
    {
        if (noAttackCD && CheckLOS())
        {
            noAttackCD = false;
            Debug.Log("BANG BANG");
            spell.Queue(spellSpawn.transform);
            spell.Use(transform.forward);
            Invoke("resetAttack", spell.useCooldown);
        }     
    }

    protected virtual void Roam()
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

    protected virtual void SearchDest()
    {
        float z = Random.Range(-roamRange, roamRange);
        float x = Random.Range(-roamRange, roamRange);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);

        if (Physics.Raycast(destPoint, Vector3.down, groundLayer))
        {
            hasDest = true;
        }
    }

    protected bool CheckLOS()
    {
        RaycastHit hit;
        Physics.Linecast(transform.position, player.transform.position, out hit);
        Debug.Log(hit.transform.gameObject.layer + " " + playerLayer);
        return hit.transform.gameObject.layer == playerLayer;

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
