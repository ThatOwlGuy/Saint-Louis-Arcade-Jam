using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(InputHelper))]
public class Mage : MonoBehaviour {

    [SerializeField]
    private MageStats stats;
    private Health hp;
    private PlayerMovement movement;
    private InputHelper input;

    //Gets the health and player movement
    private void Awake()
    {
        //Get health and movement scripts on the gameObject
        hp = GetComponent<Health>();
        movement = GetComponent<PlayerMovement>();
        input = GetComponent<InputHelper>();

        //Set the stats as appropriate
        hp.maxHealth = stats.hp;
        hp.currentHealth = hp.maxHealth;
        movement.speed = stats.movementSpeed;
    }

    //Start coroutines for primary and secondary spells
    private void Start()
    {
        StartCoroutine(PrimarySpell());
        StartCoroutine(SecondarySpell());
    }

    //fire primary spell when button 1 is pressed (and use timeOut)
    private IEnumerator PrimarySpell()
    {
        while (true)
        {
            if (Input.GetKey(input.button1))
            {
                FirePrimarySpell();
                yield return new WaitForSeconds(stats.primarySpell.timeOut);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    //fire primary spell when button 2 is pressed (and use timeOut)
    private IEnumerator SecondarySpell()
    {
        while (true)
        {
            if (Input.GetKey(input.button2))
            {
                FireSecondarySpell();
                yield return new WaitForSeconds(stats.primarySpell.timeOut);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    //
    private void FirePrimarySpell()
    {
        stats.primarySpell.CastSpell();
    }

    private void FireSecondarySpell()
    {
        stats.primarySpell.CastSpell();
    }
}
