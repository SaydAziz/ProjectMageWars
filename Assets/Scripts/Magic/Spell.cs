using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Spell : ScriptableObject
{
    public float useCooldown;
    public float queueTime;
    public float timeAlive;
    public float healthChange;
    public GameObject prefab;

    public virtual void Queue(Transform transform) { }
    public virtual void Use(Vector3 useDir) { }
    public virtual void Discard() { }

}
