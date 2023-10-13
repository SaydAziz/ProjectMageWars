using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;

    Vector3 destPoint;
    bool hasDest;
    float roamRange;
    float range = 30;


    private void FixedUpdate()
    {
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
}
