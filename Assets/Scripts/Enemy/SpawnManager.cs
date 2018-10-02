using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public Spawner[] spawnerList;
    private bool spawning;

	// Use this for initialization
	void Start () {
        spawning = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!spawning)
        {
            spawning = true;
            StartCoroutine(SpawnWait(5.0f));
        }
	}

    IEnumerator SpawnWait(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (Spawner spawner in spawnerList)
        {
            spawner.Spawn(0);
        }
        spawning = false;
        yield return 0;
    }
}
