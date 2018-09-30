using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if(stats.type == EnemyStats.AIController.Charge)
        {
            StartCoroutine(ChargeBehavior());
        }
        if (stats.type == EnemyStats.AIController.Shoot)
        {
            StartCoroutine(ShootBehavior());
        }
        else if (stats.type == EnemyStats.AIController.Slash)
        {
            print("A slash behavior");
            StartCoroutine(SlashBehavior());
        }
    }

    private IEnumerator ChargeBehavior()
    {
        //Start moving when we begin
        rb.velocity = Vector3.down;
        while (true)
        {
            //See where the closest mage is
            float distance = Vector3.Distance(ClosestMage(), transform.position);

            //If we're int eh attack radius...
            if (distance <= stats.attackRadius)
            {

                //Give a beat for rotating
                yield return new WaitForSeconds(1.5f);

                //And charge the player
                rb.velocity = ClosestMage() - transform.position * stats.speed * 2;
                CastAreaSpell();
            }

            //wait for the refresh to start again
            yield return new WaitForSeconds(stats.refreshRate);
        }
    }

    private IEnumerator ShootBehavior()
    {
        yield return new WaitForEndOfFrame();

        Animator animator = GetComponent<Animator>();

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
        {
            if (ClosestMage().y > transform.position.y)
                rb.velocity = Vector3.up;
            else
                rb.velocity = Vector3.down;
            yield return new WaitForSeconds(stats.refreshRate);
        }

        CastProjectile();
    }

    private IEnumerator SlashBehavior()
    {
        while (true)
        {
            float distance = Vector3.Distance(ClosestMage(), transform.position);

            if (distance <= stats.attackRadius)
            {
                rb.velocity = (ClosestMage() - transform.position) * stats.speed;

                if (distance < 0.75f)
                {
                    CastAreaSpell();
                }
            }
            else
            {
                rb.velocity = Vector3.down;
            }

            yield return new WaitForSeconds(stats.refreshRate);
        }
    }


    #region Common Behaviours
    private void CastAreaSpell()
    {
        Rigidbody2D projectile;
        projectile = Instantiate(stats.attack.gameObject, transform.position, transform.rotation).GetComponent<Rigidbody2D>();

        //Set the projectile's caster and damage
        MagicProjectile mp = projectile.GetComponent<MagicProjectile>();
        mp.SetCaster(gameObject);
        mp.SetDamage(stats.damage);

        Destroy(projectile.gameObject, stats.attackDuration);
    }

    private void CastProjectile()
    {
        //Set the direction of the projectile (since shooty bois will be ont he left or right side of the screen)
        Quaternion projectileDirection;
        if (transform.position.x > 0)
            projectileDirection = Quaternion.Euler(Vector3.left);
        else
            projectileDirection = Quaternion.Euler(Vector3.right);

        //Instantiate the projectile
        Rigidbody2D projectile;
        projectile = Instantiate(stats.attack.gameObject, transform.position, transform.rotation).GetComponent<Rigidbody2D>();

        //Set the projectile's caster and damage
        MagicProjectile mp = projectile.GetComponent<MagicProjectile>();
        mp.SetCaster(gameObject);
        mp.SetDamage(stats.damage);

        //and now, actually shoot the projectile
        projectile.velocity = transform.up * stats.speed;
        Destroy(projectile.gameObject, stats.attackDuration);
    }

    private Vector3 ClosestMage()
    {
        //look for mages
        GameObject[] mages = GameObject.FindGameObjectsWithTag("Player");

        //return null if there are no mages
        if (mages.Length == 0)
        {
            print("NO mages found!");
            return Vector3.zero;
        }

        //If there is only one mage, then that is the closest mage
        GameObject closestMage = mages[0];

        //If both mages are present, then see which one is closer
        if (mages.Length != 1)
        {
            float minDistance = float.MaxValue;

            foreach (GameObject mage in mages)
            {
                float newDistance = Vector3.Distance(transform.position, mage.transform.position);
                if (newDistance < minDistance)
                {
                    minDistance = newDistance;
                    closestMage = mage;
                }
            }
        }

        return closestMage.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Mage>() != null)
            other.GetComponent<Health>().DealDamage(stats.damage);
    }
    #endregion END: Common Behaviours
}
