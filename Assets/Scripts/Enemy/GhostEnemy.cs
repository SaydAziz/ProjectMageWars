using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : EnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        //attackCD = 2f;
        roamRange = 50;
        sightRange = 50;
        attackRange = 25;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void Roam()
    {
        base.Roam();
    }

    protected override void SearchDest()
    {
        base.SearchDest();
    }

}
