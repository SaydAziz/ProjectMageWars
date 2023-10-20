using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour, IDamageable
{
    public Spell leftHand, rightHand;
    [SerializeField] VisualController vController;

    private float maxHealth = 100;
    public float health { get; set; }

    public bool isDead = false;

    private void Awake()
    {
        health = 100;
    }

    private void FixedUpdate()
    {
        vController.UpdateHealth(health/maxHealth);
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
        isDead = true;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
