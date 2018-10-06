using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Attack : MonoBehaviour
{
    public int damage;
    public float duration;
    private Rigidbody2D rb;
    public bool hasKnockBack;
    public bool destroyOnTouch;
    private DeathHandler.Combatant attacker = DeathHandler.Combatant.NULL;

    private void Start()
    {
        Destroy(gameObject, duration);
    }

    public void EstablishSource(DeathHandler.Combatant source)
    {
        attacker = source;
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
        {
            //If they are then apply damage
            ApplyDamage(other.GetComponent<Health>());

            //If we hit, then dissapear if that's what we're supposed to do
            if (destroyOnTouch)
                Destroy(gameObject);

            //If we hit, then push the other character if that's what we're supposed to do
            if (hasKnockBack)
                KnockBack(other.GetComponent<Rigidbody2D>());
        }
    }

    private void KnockBack(Rigidbody2D adversary)
    {
        adversary.velocity = (adversary.transform.position - transform.position).normalized * 10f;
    }

    private bool IsEnemy(string otherTag)
    {
        return otherTag != tag;
    }

    private void ApplyDamage(Health enemyHp)
    {
        //Sometimes enemies will die in the middle of an attack
        if (enemyHp == null)
            return;

        //If the enemy is recovering then we can't hurt them again either
        if (enemyHp.inRecovery)
            return;

        //If this is going to kill it, we'll report it to the death handler
        if(enemyHp.currentHealth - damage <= 0)
        {
            //Figure out which kind of death to report
            DeathHandler.Combatant victim = DeathHandler.Combatant.NULL;

            //If it's tagged enemy, then its a lizard person
            if (enemyHp.tag == "Enemy")
                victim = DeathHandler.Combatant.LizardPerson;
            else    //Otherwise, we'll find out which player based on the mage's playerIndex
            {
                Mage player = enemyHp.GetComponent<Mage>();

                if (player.playerIndex == Player.Index.One)
                    victim = DeathHandler.Combatant.ThermalMage;

                if (player.playerIndex == Player.Index.Two)
                    victim = DeathHandler.Combatant.ElectromagneticMage;
            }

            ReportDeath(victim);
        }

        enemyHp.DealDamage(damage);
    }

    private void ReportDeath(DeathHandler.Combatant victim)
    {
        print(attacker + " killed " + victim);

        DeathHandler dh = FindObjectOfType<DeathHandler>();
        dh.RegisterDeath(attacker, victim);
    }
}