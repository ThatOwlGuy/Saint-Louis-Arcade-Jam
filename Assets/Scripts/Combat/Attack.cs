using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Attack : MonoBehaviour
{
    public int damage;
    public float duration;
    private Rigidbody2D rb;

    private void Start()
    {
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //If they're not a combatant, don't even bother
        if (other.tag != "Enemy" && other.tag != "Player")
            return;

        if(IsEnemy(other.tag))
            //If they are then apply damage
            ApplyDamage(other.GetComponent<Health>());

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //If they're not a combatant, don't even bother
        if (other.tag != "Enemy" && other.tag != "Player")
            return;

        if (IsEnemy(other.tag))
            //If they are then apply damage
            ApplyDamage(other.GetComponent<Health>());
    }

    private bool IsEnemy(string otherTag)
    {
        return otherTag != tag;
    }


    private void ApplyDamage(Health enemyHp)
    {
        if (enemyHp.inRecovery)
            return;

        //If this is going to kill it, we'll instead report it to the death handler
        if(enemyHp.currentHealth - damage <= 0)
        {
            ReportDeath();
        }

        enemyHp.DealDamage(damage);
    }

    protected virtual void ReportDeath()
    {
        DeathHandler dh = FindObjectOfType<DeathHandler>();
    }
}