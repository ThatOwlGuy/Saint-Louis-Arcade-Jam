using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(InputHelper))]
public class Mage : Player {

    #region Variables
    private Health hp;
    private PlayerMovement movement;

    public Spell[] spellSlots = new Spell[2];
    #endregion END: Variables

    #region Initialization

    //Gets the health and player movement
    private void Start()
    {
        //Get health and movement scripts on the gameObject
        hp = GetComponent<Health>();
        movement = GetComponent<PlayerMovement>();
    }

    #endregion END: Initialization

    private void Update()
    {
        //Primary Attack
        if (Input.GetKey(input.button1))
        {
            if (spellSlots[0].onCoolDown == false)
            {
                CastSpell(0);
            }
        }

        //Secondary Attack
        if (Input.GetKey(input.button2))
        {
            if (spellSlots[1].onCoolDown == false)
            {
                CastSpell(1);
            }
        }
    }

    private void CastSpell(int spellIndex)
    {
        //Instantiate casted Magic
        GameObject magic;
        magic = Instantiate(spellSlots[spellIndex].attack.gameObject, transform.position, Quaternion.identity);

        //Start cooldown
        StartCoroutine(CoolDown(spellIndex));
    }

    private IEnumerator CoolDown(int slotIndex)
    {
        spellSlots[slotIndex].onCoolDown = true;

        yield return new WaitForSeconds(spellSlots[slotIndex].coolDownTime);

        spellSlots[slotIndex].onCoolDown = false;
    }
}

[System.Serializable]
public struct Spell
{
    public Attack attack;
    public float coolDownTime;
    public bool onCoolDown;
}
