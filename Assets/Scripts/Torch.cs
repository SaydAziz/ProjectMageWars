using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject fire;
    public float health { get; set; }

    public void TakeDamage(float damage)
    {
        fire.gameObject.SetActive(true);
        GameManager.Instance.agent.EnableNextTrigger();

    }

}
