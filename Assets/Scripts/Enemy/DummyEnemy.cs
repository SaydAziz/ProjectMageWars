using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : EnemyController
{
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        health = 999999;
        //attackCD = 2f;
        roamRange = 0;
        sightRange = 30;
        attackRange = 30;

        noAttackCD = false;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void FixedUpdate()
    {
        seesPlayer = Vector3.Distance(transform.position, player.transform.position) < sightRange;
        canAttack = Vector3.Distance(transform.position, player.transform.position) < attackRange;
        if (health <= 0) Die();

        if (seesPlayer && canAttack)
        {
            if (noAttackCD && CheckLOS(player.transform))
            {
                Attack();
            }

        }
    }

    protected override void Attack()
    {
        anim.SetTrigger("Shoot");
        noAttackCD = false;
        //Debug.Log("BANG BANG");
        Invoke("CompleteAttack", 1);
    }

    protected override void CompleteAttack()
    {
        spell.Queue(spellSpawn.transform);
        spellSpawn.transform.LookAt(player.transform.position);
        spell.Use(spellSpawn.transform.forward);
        Invoke("resetAttack", spell.useCooldown);
    }

    protected override void Roam()
    {
        base.Roam();
    }

    protected override void SearchDest()
    {
        base.SearchDest();
    }

    public override void TakeDamage(float damage)
    {
        Debug.Log("DUMMY WAS HIT");

        GameManager.Instance.agent.EnableNextTrigger();
    }

    public void EnableFight(bool fight)
    {
        CancelInvoke();
        noAttackCD = fight;
    }
}
