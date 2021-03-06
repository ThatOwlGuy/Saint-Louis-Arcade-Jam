﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionHighScore : MonoBehaviour {

    public Text higherScore;
    public Text lowerScore;

    private void Start()
    {
        int p1 = ScoreKeeping.currentPlayerScores[0];
        int p2 = ScoreKeeping.currentPlayerScores[1];

        if(p1 > p2)
        {
            higherScore.text = "Mage 1" + '\n' + '\n' + p1;
            lowerScore.text = "Mage 2" + '\n' + '\n' + p2;
        }
        else
        {
            lowerScore.text = "Mage 1" + '\n' + '\n' + p1;
            higherScore.text = "Mage 2" + '\n' + '\n' + p2;
        }

        StartCoroutine(WaitForEndOfAnimation());
    }

    private IEnumerator WaitForEndOfAnimation()
    {
        yield return new WaitForSeconds(2.0f);

        FindObjectOfType<EndScreenManagement>().screenTaskComplete = true;
    }
}
