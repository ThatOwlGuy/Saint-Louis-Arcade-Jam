using System.Collections;
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
    private Animator animator;

    private void Start()
    {
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