using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject corpse;

    public void DealDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Dead()
    {
        GameObject clone;
        clone = Instantiate(corpse, transform.position, Quaternion.identity);

        if (tag == "Player")
        {
            DeathHandler dh = FindObjectOfType<DeathHandler>();
            dh.RegisterPlayerDeath(name);
        }

        Destroy(gameObject);
    }
}