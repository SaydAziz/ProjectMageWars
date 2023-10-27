using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeEnemy : EnemyController
{

    [SerializeField] Collider hitbox;
    float damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        attackCD = .5f;
        roamRange = 10;
        sightRange = 15;
        attackRange = 2;
        agent.speed = 5;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
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
            Debug.Log("BITE");
            hitbox.enabled = true;
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
