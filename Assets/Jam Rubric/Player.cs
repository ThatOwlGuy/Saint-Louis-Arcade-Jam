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

	}
}
