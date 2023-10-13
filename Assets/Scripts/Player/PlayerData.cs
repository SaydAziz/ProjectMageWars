using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour, IDamageable
{
    public Spell leftHand, rightHand;

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
    }

    public void EquipSpell(Spell spell, bool isLeft)
    {
        if (isLeft)
        {
            leftHand = spell;
        }
        else if (!isLeft)
        {
            rightHand = spell;
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
