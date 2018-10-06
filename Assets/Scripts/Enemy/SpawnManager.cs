using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public Spawner[] spawnerList;
    private bool spawning;
    private float spawnTime;

	// Use this for initialization
	void Start () {
        spawnTime = 12.0f;
        spawning = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!spawning)
        {
            spawning = true;
            StartCoroutine(SpawnWait(spawnTime));
            if(spawnTime > 1.0f)
            {
                spawnTime = spawnTime - 0.4f;
            }
        }
	}

    IEnumerator SpawnWait(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (Spawner spawner in spawnerList)
        {
            spawner.Spawn(Mathf.FloorToInt(Random.Range(0.0f, 2.99f)));
        }
        spawning = false;
        yield return 0;
    }
}
