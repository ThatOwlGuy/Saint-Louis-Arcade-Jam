using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public enum Index
    {
        One = 1,
        Two = 2
    }    

    public int score = 0;
    protected InputHelper input;
    public Index playerIndex = Index.One;

    // Update is called once per frame
    virtual public void Update () {
		if (Input.GetKeyDown(input.button1))
        {
            score += 1;
        }
        if (Input.GetKeyDown(input.button2))
        {
            score -= 1;
        }
        if (score < 0)
        {
            score = 0;
        }
        if (score > 999999)
        {
            score = 999999;
        }
	}
}
