using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour {

    public void RegisterPlayerDeath(string player)
    {
        print(player + " is dead!");
    }
}
