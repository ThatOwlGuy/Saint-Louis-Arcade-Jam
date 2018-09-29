using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiScoreDisplay : MonoBehaviour {

    public Player playerOne;
    public Player playerTwo;

    private Text text;
    private int highScore = 0;

    private Vector2Int localScore;

    void Start () {
        text = GetComponent<Text>();
        localScore = new Vector2Int(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (playerOne != null)
            localScore.x = playerOne.score;

        if (playerTwo != null)
            localScore.y = playerTwo.score;

        highScore = Math.Max(Math.Max(localScore.x, localScore.y), highScore);
        text.text = string.Format("Hi-{0}", highScore.ToString("D6"));
	}
}
