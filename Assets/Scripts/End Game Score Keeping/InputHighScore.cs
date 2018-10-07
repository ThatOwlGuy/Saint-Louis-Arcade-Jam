using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHighScore : MonoBehaviour {

    public Text[] characterEntries = new Text[3];

    private bool[] highScoresEntered;

    private void Start()
    {
        int scoresToEnter = 0;

        if (ScoreKeeping.IsNewHighScore(ScoreKeeping.currentPlayerScores[0]))
            scoresToEnter++;

        if (ScoreKeeping.IsNewHighScore(ScoreKeeping.currentPlayerScores[2]))
            scoresToEnter++;

        highScoresEntered = new bool[scoresToEnter];
    }

    private void Update()
    {

    }
}
