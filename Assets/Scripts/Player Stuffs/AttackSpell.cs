using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Magic/Spell", order = 2)]
public class AttackSpell : ScriptableObject {

	public enum SpellType
    {
        Projectile,
        Area
    }

    public SpellType type;
    public MagicProjectile spellPrefab;
    public int damage;
    public float speedOrDuration;
    public int timeOut;
}
