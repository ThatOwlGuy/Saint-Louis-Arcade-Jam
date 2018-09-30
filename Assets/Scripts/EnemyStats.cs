using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy", order = 0)]
public class EnemyStats : ScriptableObject {

    public Sprite spriteSheet;
    public int hp;
    public int damage;
    public MagicProjectile attack;
    public float attackDuration;
    public float speed;
    public float attackRadius;
    public enum AIController
    {
        Charge = 5,
        Shoot = 25,
        Slash = 10
    }
    public AIController type;
    public float refreshRate;   //In seconds. Not per second
}
