using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject corpse;
    public bool inRecovery;

    public void DealDamage(int damage)
    {
        currentHealth -= damage;

        StartCoroutine(Recovery());

        ValidateHealth();
    }

    private void ValidateHealth()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (currentHealth <= 0)
            Dead();
    }

    private IEnumerator Recovery()
    {
        inRecovery = true;

        yield return new WaitForSeconds(1.0f);

        inRecovery = false;
    }

    public void Dead()
    {
        GameObject clone;
        clone = Instantiate(corpse, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}