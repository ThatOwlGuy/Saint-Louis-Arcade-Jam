using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy", order = 0)]
public class EnemyStats : ScriptableObject {

    public Sprite spriteSheet;
    public int hp;
    public int damage;
    public float speed;
    public enum AIController
    {
        FastBoi,
        ShootyBoi,
        SlowBoi,
        DumbBoi
    }
    public AIController type;
    public float refreshRate;   //In seconds. Not per second
}
