using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour {

    public void RegisterPlayerDeath(Player player)
    {
        print(player.name + " is dead!");
    }

    internal void RegisterEnemyDeath(EnemyStats.AIController type, Player player)
    {
        player.score += (int)type;
        print("Register Enemy Death is not implemented!");
    }
}
