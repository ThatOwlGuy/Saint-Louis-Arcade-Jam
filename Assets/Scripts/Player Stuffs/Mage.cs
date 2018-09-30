using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(InputHelper))]
public class Mage : Player {

    #region Variables
    [SerializeField]
    private MageStats stats;
    private Health hp;
    private PlayerMovement movement;
    #endregion END: Variables

    #region Initialization

    //Gets the health and player movement
    [ExecuteInEditMode]
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

        //For now we're just going to apply the entire sheet since animations aren't in yet
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = stats.spriteSheet;

        //We're also just going to color them according to which player it is.
        if (playerIndex == Index.One)
            sprite.color = new Color(0f, 0f, 0.75f, 1f);
        else
            sprite.color = new Color(1f, 0.5f, 0f, 1f);
    }

    #endregion END: Initialization

    #region SpellCasting

    #region Primary & Secondary Methods
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

    //Fires a Primary Spell
    private void FirePrimarySpell()
    {
        CastSpell(stats.primarySpell);
        print("Fired primary Spell");
    }

    //Fires a Secondary Spell
    private void FireSecondarySpell()
    {
        CastSpell(stats.secondarySpell);
    }
    #endregion END: Primary & Secondary Methods

    #region Type Spell Casting
    //Casts a spell dependent upon its type
    private void CastSpell(AttackSpell spell)
    {
        if(spell.type == AttackSpell.SpellType.Projectile){
            CastProjectile(spell);
        }
        else
        {
            CastArea(spell);
        }
    }

    //Casts a projectile
    private void CastProjectile(AttackSpell spell)
    {
        GameObject clone;
        Vector3 spawnPoint = transform.position + Vector3.up * 0.25f;
        clone = Instantiate(spell.spellPrefab.gameObject, spawnPoint, Quaternion.identity);

        //Projectile's hold speed
        MagicProjectile mp = clone.GetComponent<MagicProjectile>();

        mp.SetCaster(gameObject);
        mp.SetDamage(spell.damage);

        clone.GetComponent<Rigidbody2D>().velocity = spell.speedOrDuration * Vector2.up;
    }

    //Casts a Spell in a designated area
    private void CastArea(AttackSpell spell)
    {
        Vector3 spawnPoint = transform.position + Vector3.up * 0.25f;
        //Projectile's hold speed
        MagicProjectile mp;
        mp = Instantiate(spell.spellPrefab.gameObject, spawnPoint, Quaternion.identity).GetComponent<MagicProjectile>();
        mp.SetCaster(gameObject);
        mp.SetDamage(spell.damage);

        Destroy(mp.gameObject, spell.speedOrDuration);
    }
    #endregion END: Type Spell Casting

    #endregion END: SpellCasting
}
