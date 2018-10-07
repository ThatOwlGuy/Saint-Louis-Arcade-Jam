using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{ 
    public enum Combatant
    {
        NULL,
        ThermalMage = 0,
        ElectromagneticMage = 1,
        LizardPerson
    }

    public Player p1;
    public Player p2;

    public void RegisterDeath(Combatant attacker, Combatant victim)
    {
        //If the lizard killed, then we just register that a player died
        if (attacker == Combatant.LizardPerson)
        {
            return;
        }
        else
        {
            if (attacker == Combatant.ThermalMage)
                p1.score += 10;
                

            if (attacker == Combatant.ElectromagneticMage)
                p2.score += 10;
        }
    }

    private void LateUpdate()
    {
        if(FindObjectsOfType<Mage>().Length == 0)
        {
            if (FindObjectsOfType<Mage>().Length == 0)
            {
                ScoreKeeping.currentPlayerScores = new int[2];
                ScoreKeeping.currentPlayerScores[0] = p1.score;
                ScoreKeeping.currentPlayerScores[1] = p2.score;
                SceneManager.LoadScene("End Screen");
            }
        }
    }
}