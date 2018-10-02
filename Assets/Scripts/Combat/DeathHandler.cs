using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{ 
    public enum Combatant
    {
        ThermalMage,
        ElectromagneticMage,
        LizardPerson
    }

    public void RegisterDeath(Combatant attacker, Combatant victim)
    {
        //If the lizard killed, then we just register that a player died
        if (attacker == Combatant.LizardPerson)
        {
            if (victim == Combatant.ThermalMage)
                print("Player one died.");
            else if (victim == Combatant.ElectromagneticMage)
                print("Player two died.");
        }
        else
        {
            if (attacker == Combatant.ThermalMage)
            {
                print("Add to Player 1's score");
            }

            if (attacker == Combatant.ElectromagneticMage)
            {
                print("Add to Player 2's score");
            }
        }
    }
}