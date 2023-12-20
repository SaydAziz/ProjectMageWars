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
    public bool canDash; //probably should get set this stuff


    private void Start()
    {
        canDash = false;
    }

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
        vController.SetDashIcon(canDash);
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
        if (!isDead)
        {
            GameManager.Instance.DeathStuff();
            health = 1;
            isDead = true;
            vController.BlackOut(1);
            Invoke("hitCP", 2);
        }
    }

    private void hitCP()
    {
        health = 100;
        isDead = false;
        GameManager.Instance.ResetCheckpoint();
        vController.BlackOut(-1);
        
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
