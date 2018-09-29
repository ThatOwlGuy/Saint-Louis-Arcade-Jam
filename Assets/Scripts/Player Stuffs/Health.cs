using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject corpse;

    public void DealDamage(int damage)
    {
        currentHealth -= damage;

        ValidateHealth();
    }

    private void ValidateHealth()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (currentHealth <= 0)
            Dead();
    }

    public void Dead()
    {
        GameObject clone;
        clone = Instantiate(corpse, transform.position, transform.rotation);

        if (tag == "Player")
        {
            DeathHandler dh = FindObjectOfType<DeathHandler>();
            dh.RegisterPlayerDeath(GetComponent<Player>());
        }

        Destroy(gameObject);
    }
}