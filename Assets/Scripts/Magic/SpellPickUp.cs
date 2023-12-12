using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPickUp : MonoBehaviour
{
    [SerializeField] Spell spell;
    [SerializeField] AudioSource spellSound;
    float startingY;

    private void Start()
    {
        spellSound.pitch = 1.01f;
        startingY = transform.position.y;
        StartCoroutine(animatePickup());
    }

    IEnumerator animatePickup()
    {
        while(true)
        {
            transform.Rotate(Vector3.up, 60 * Time.deltaTime, Space.World);
            transform.position = new Vector3(transform.position.x,  startingY + (Mathf.Sin(Time.time) * 0.1f), transform.position.z);
            yield return null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerData>().EquipSpell(spell, false);
        Destroy(gameObject);
    }
}
