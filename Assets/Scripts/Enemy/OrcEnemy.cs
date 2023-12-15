using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcEnemy : EnemyController
{
    [SerializeField] Collider hitbox;
    [SerializeField] Animator animator;
    float damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        attackCD = 1f;
        roamRange = 10;
        sightRange = 25;
        attackRange = 2;
        agent.speed = 5;
        health = 50;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void FixedUpdate()
    {
        seesPlayer = Vector3.Distance(transform.position, player.transform.position) < sightRange;
        //Physics.CheckSphere(transform.position, sightRange, playerLayer);
        canAttack = Vector3.Distance(transform.position, player.transform.position) < attackRange;
        //Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if (health <= 0) Die();

        if (seesPlayer && canAttack)
        {
            if (noAttackCD && CheckLOS(player.transform))
            {
                agent.SetDestination(transform.position);
                transform.LookAt(player.transform.position);
                Attack();
            }
        }
        else if (seesPlayer)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            Roam();
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 7)
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }

    protected override void Attack()
    {
        if (noAttackCD)
        {
            noAttackCD = false;
            Debug.Log("ATTACK");
            hitbox.enabled = true;
            animator.SetTrigger("Attack");
            Invoke("OffHitBox", 0.1f);
            Invoke("resetAttack", attackCD);
        }
    }

    protected override void Roam()
    {
        base.Roam();
    }
    protected override void SearchDest()
    {
        base.SearchDest();
    }

    private void OffHitBox()
    {
        hitbox.enabled = false;
    }
}
