using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells/Projectile Spell")]
public class ProjectileSpell : Spell
{
    public float travelSpeed;
    private GameObject spellCache;
    private Rigidbody spellRB;
    private Projectile projectile;

    public override void Queue(Transform transform)
    {
        spellCache = Instantiate(prefab, transform);
        spellRB = spellCache.GetComponent<Rigidbody>();
        projectile = spellCache.GetComponent<Projectile>();
    }
    public override void Use()
    {
        spellRB.detectCollisions = true;
        spellCache.gameObject.transform.parent = null;
        spellRB.freezeRotation = false;
        spellRB.constraints = RigidbodyConstraints.None;
        spellRB.velocity = spellCache.gameObject.transform.parent.transform.forward * travelSpeed;

        projectile.Shoot();
    }
}
        
