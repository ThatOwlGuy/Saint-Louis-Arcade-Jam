using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mage", menuName = "Magic/Mage", order = 1)]
public class MageStats : ScriptableObject {

    public Sprite spriteSheet;
    public int hp;
    public float movementSpeed;
    public AttackSpell primarySpell;
    public AttackSpell secondarySpell;
}
