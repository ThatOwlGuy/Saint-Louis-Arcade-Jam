using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{ 
    public enum Combatant
    {
        NULL,
        ThermalMage,
        ElectromagneticMage,
        LizardPerson
    }

    public Player p1;
    public Player p2;

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
                p1.score += 10;
                

            if (attacker == Combatant.ElectromagneticMage)
                p2.score += 10;
        }
    }
}