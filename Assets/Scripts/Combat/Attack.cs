using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour
{
    public int damage;
    public float duration;
    public bool hasKnockBack;
    public bool destroyOnTouch;
    private DeathHandler.Combatant attacker = DeathHandler.Combatant.NULL;
    private Animator animator;

    private void Start()
    {
        EstablishSource();

        animator = gameObject.GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Destroy(gameObject, duration);
        }
        else
        {
            animator.SetBool("start", true);
            StartCoroutine(RemoveAfterAnimation());
        }
    }

    IEnumerator RemoveAfterAnimation()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Exit"))
        {
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(gameObject);
        yield return null;
    }

    public void EstablishSource()
    {
        if (tag == "Enemy")
            attacker = DeathHandler.Combatant.LizardPerson;

        if (tag == "Player")
        {
            Mage[] mages = FindObjectsOfType<Mage>();

            for(int i = 0; i < mages.Length; i++)
            {
                for(int j = 0; j < mages[i].spellSlots.Length; j++)
                {
                    
                    if(name == mages[i].spellSlots[j].attack.name + "(Clone)")
                    {
                        if (mages[i].playerIndex == Player.Index.One)
                            attacker = DeathHandler.Combatant.ThermalMage;
                        else
                            attacker = DeathHandler.Combatant.ElectromagneticMage;
                    }
                }
            }
        }

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