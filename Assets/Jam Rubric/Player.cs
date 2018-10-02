using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputHelper))]
public class Player : MonoBehaviour {

    public enum Index
    {
        One = 1,
        Two = 2
    }    

    public int score = 0;
    protected InputHelper input;
    public Player.Index playerIndex = Player.Index.One;

    private void Awake()
    {
        input = GetComponent<InputHelper>();
    }
}
