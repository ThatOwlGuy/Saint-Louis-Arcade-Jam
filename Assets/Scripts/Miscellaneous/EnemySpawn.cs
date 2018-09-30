using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public Enemy[] enemies;
    public float spawnRate;

    private CountDownTimer timer;

    public void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        int maxTime = (int)timer.initialTime;
        while(timer.timeLeft > 1)
        {
            
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
