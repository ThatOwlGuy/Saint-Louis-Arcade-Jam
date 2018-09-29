using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour {

    public EnemyStats stats;
    public Health hp;
    private Rigidbody2D rb;
    public MagicProjectile projectile;

    public void Start()
    {
        hp = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();

        hp.maxHealth = stats.hp;
        hp.currentHealth = hp.maxHealth;

        //For now we're just going to apply the entire sheet since animations aren't in yet
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = stats.spriteSheet;
        sprite.color = new Color(1.0f, 0f, 0f, 1.0f);

        if (stats.type == EnemyStats.AIController.FastBoi)
        {
            StartCoroutine(FastBoiBehaviour());
        }else if (stats.type == EnemyStats.AIController.ShootyBoi)
        {
            StartCoroutine(ShootyBoiBehaviour());
        }
        else if (stats.type == EnemyStats.AIController.SlowBoi)
        {
            StartCoroutine(SlowBoiBehaviour());
        }
        else
        {
            DumbBoiBehaviour();
        }
    }

    private IEnumerator FastBoiBehaviour()
    {
        while(hp.currentHealth > 0)
        {
            rb.velocity = transform.forward * stats.speed;
            if (ClosestMage() != null)
                StartCoroutine(RotateTowards(ClosestMage().transform.position));
            yield return new WaitForSeconds(stats.refreshRate);
        }
    }

    private IEnumerator ShootyBoiBehaviour()
    {
        while (hp.currentHealth > 0)
        {
            if(ClosestMage() != null)
                StartCoroutine(RotateTowards(ClosestMage().transform.position));

            if (Vector2.Distance(transform.position, ClosestMage().transform.position) < 5)
                Shoot();
            else
            {
                rb.velocity = transform.forward * stats.speed;
            }

            yield return new WaitForSeconds(stats.refreshRate);
        }

    }

    private void Shoot()
    {
        //Stop to shoot
        rb.velocity = Vector2.zero;
        
        Rigidbody2D clone;
        clone = Instantiate(projectile.gameObject, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();

        clone.velocity = transform.forward * 5f;
    }

    private IEnumerator SlowBoiBehaviour()
    {
        while (hp.currentHealth > 0)
        {
            if (ClosestMage() != null)
                StartCoroutine(RotateTowards(ClosestMage().transform.position));
            rb.velocity = transform.forward * stats.speed;
            yield return new WaitForSeconds(stats.refreshRate);
        }
    }

    private void DumbBoiBehaviour()
    {
        rb.velocity = new Vector3(0f, -stats.speed, 0f);
    }

    #region Common Behaviours
    private Mage ClosestMage()
    {
        //look for mages
        Mage[] mages = FindObjectsOfType<Mage>();

        //return null if there are no mages
        if(mages.Length != 0)
            return null;

        //If there is only one mage, then that is the closest mage
        Mage closestMage = mages[0];

        //If both mages are present, then see which one is closer
        if (mages.Length != 1)
        {
            float minDistance = float.MaxValue;

            foreach (Mage mage in mages)
            {
                float newDistance = Vector3.Distance(transform.position, mage.transform.position);
                if (newDistance < minDistance)
                {
                    minDistance = newDistance;
                    closestMage = mage;
                }
            }
        }

        return closestMage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Mage>() != null)
            other.GetComponent<Health>().DealDamage(stats.damage);
    }

    private IEnumerator RotateTowards(Vector3 target)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);

        while(transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    #endregion END: Common Behaviours
}
