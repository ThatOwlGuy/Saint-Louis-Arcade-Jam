using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour {

    public void RegisterPlayerDeath(Player player)
    {
        Player[] players = FindObjectsOfType<Player>();

        if (players.Length == 0)
            StartGameOver();
    }

    public void RegisterEnemyDeath(EnemyStats.AIController type, Player player)
    {
        player.score += (int)type;
        print("Register Enemy Death is not implemented!");
    }

    private void StartGameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
