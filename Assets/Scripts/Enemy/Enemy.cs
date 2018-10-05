using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour {

    #region Variables
    private Health hp;
    private Rigidbody2D rb;
    public Attack attack;

    #region Stats
    public float speed;
    public float attackRadius;
    #endregion END: END: Stats

    #region AI
    public enum AIController
    {
        Arrow,
        Charge,
        Slash
    }
    public AIController ai;
    public float refreshRate;
    #endregion END: AI
    #endregion END: Variables

    #region Initialization
    public void Start()
    {
        hp = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();

        if(ai == AIController.Arrow)
        {
            //flip enemy depending on which side of the screen they're on
            if (transform.position.x > 0)
                transform.localScale = new Vector3(-1f, 1f, 1f);

            StartCoroutine(ShootBehavior());
        }
        if (ai == AIController.Charge)
        {
            StartCoroutine(ChargeBehavior());
        }
        else if (ai == AIController.Slash)
        {
            StartCoroutine(SlashBehavior());
        }
    }
    #endregion END: Initialization

    #region Archer Lizard
    private IEnumerator ShootBehavior()
    {
        while (true)
        {
            rb.velocity = Vector3.down;

            if (Vector3.Distance(ClosestMage(), transform.position) <= attackRadius) {

                Animator animator = GetComponent<Animator>();

                if (animator != null)
                {
                    yield return new WaitForSeconds(refreshRate);

                    animator.SetBool("Fire Now", true);

                    while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
                        yield return new WaitForEndOfFrame();
                }

                FireArrow();

            }

            yield return new WaitForSeconds(refreshRate);
        }
    }

    private void FireArrow()
    {
        Rigidbody2D arrow;
        Transform arrowSpawn = transform.GetChild(0);
        arrow = Instantiate(attack.gameObject, arrowSpawn.position, arrowSpawn.rotation).GetComponent<Rigidbody2D>();
        arrow.velocity = (speed * arrowSpawn.up) + Vector3.down;
    }
    #endregion END: Archer Lizard

    #region Charge Lizard
    private IEnumerator ChargeBehavior()
    {
        //Start moving when we begin
        rb.velocity = Vector3.down;

        bool charged = false; ;

        //if we haven't yet charged
        while (!charged)
        {
            //See where the closest mage is
            float distance = Vector3.Distance(ClosestMage(), transform.position);

            //If we're in the attack radius...
            if (distance <= attackRadius)
            {
                //FOR TESTING PURPOSES
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;

                //Give 2 seconds for charge position
                yield return new WaitForSeconds(1f);

                //And charge the player
                rb.velocity = (ClosestMage() - transform.position).normalized * speed;
            }

            //wait for the refresh to start again
            yield return new WaitForSeconds(refreshRate);
        }
    }
    #endregion END: ChargeLizard

    #region Slash Lizard
    private IEnumerator SlashBehavior()
    {
        rb.velocity = Vector3.down;

        while (true)
        {
            float distance = Vector3.Distance(ClosestMage(), transform.position);

            if (distance <= attackRadius)
            {
                rb.velocity = (ClosestMage() - transform.position).normalized * speed;

                if (distance <= 2)
                {
                    SwordSlash();
                    yield return new WaitForSeconds(transform.GetChild(1).GetComponent<Attack>().duration);
                }
            }
            

            yield return new WaitForSeconds(refreshRate);
        }
    }

    private void SwordSlash()
    {
        GameObject slash;
        slash = Instantiate(attack.gameObject, transform.position, Quaternion.identity);
        slash.transform.SetParent(transform);
    }
    #endregion Slash Lizard

    #region Common Behaviours

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
        {
            Health mageHp = other.GetComponent<Health>();
            if (!mageHp.inRecovery)
            {
                //Deal damage and push away if not in recovery
                mageHp.DealDamage(1);
                KnockBack(other.GetComponent<Rigidbody2D>());
            }
        }
    }

    private void KnockBack(Rigidbody2D adversary)
    {
        adversary.velocity = (adversary.transform.position - transform.position).normalized * 10f;
    }
    #endregion END: Common Behaviours
}
