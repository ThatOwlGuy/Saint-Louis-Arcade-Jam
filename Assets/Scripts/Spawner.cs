using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] spawnableEnemies;

	// Use this for initialization
	public void Spawn (int enemyIndex) {
        Instantiate(spawnableEnemies[enemyIndex], this.transform.position, Quaternion.identity);
	}
}
