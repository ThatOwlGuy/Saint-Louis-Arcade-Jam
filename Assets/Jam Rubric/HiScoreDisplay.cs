using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiScoreDisplay : MonoBehaviour {

    private Text text;
    private int highScoreInt;

    private Vector2Int localScore;

    void Start () {
        text = GetComponent<Text>();
        highScoreInt = PlayerPrefs.GetInt("highScore", 0);
        text.text = string.Format("Best Score {0}", highScoreInt.ToString("D6"));
    }
	
	// Update is called once per frame
	void Update () {

        
	}
}
