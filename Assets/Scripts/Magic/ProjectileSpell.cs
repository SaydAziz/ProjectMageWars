using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells/Projectile Spell")]
public class ProjectileSpell : Spell
{
    public float travelSpeed;
    private GameObject spellCache;
    private Projectile projectile;

    public void Start()
    {
        
    }

    public override void Queue(Transform transform)
    {
        Debug.Log("QUEUEUEUEUEUUE");
        spellCache = Instantiate(prefab, transform);
        projectile = spellCache.GetComponent<Projectile>();
        projectile.SetData(travelSpeed, timeAlive, healthChange);
    }
    public override void Use()
    {
        projectile.Shoot();
        spellCache.transform.Rotate(-90, 0, 0);

    }
}
        
