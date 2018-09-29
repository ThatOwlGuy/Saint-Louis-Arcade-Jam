﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    private GameObject caster;
    private int damage;

    public void SetCaster(GameObject newCaster)
    {
        caster = newCaster;
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == caster.tag)
            return;

        print(other.name);

        Health ohp = other.GetComponent<Health>();

        if (ohp != null)
        {
            print(other.name + " has health");
            if (ohp.currentHealth - damage <= 0)
                FindObjectOfType<DeathHandler>().RegisterEnemyDeath(ohp.GetComponent<Enemy>().stats.type, caster.GetComponent<Player>());

            ohp.DealDamage(damage);
        }
        else
        {
            print(other.name + " does not have health");
        }
    }
}