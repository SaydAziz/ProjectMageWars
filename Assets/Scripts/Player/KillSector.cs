using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<PlayerData>().TakeDamage(1000);
    }
}
