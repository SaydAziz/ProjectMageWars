using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Spell leftHand, rightHand;

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
}
