using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;

public static class ScoreKeeping {

    public static int[] currentPlayerScores = { 20, 10 };
    public static PlayerScore[] highScores;

    public static bool IsNewHighScore(int newScore)
    {
        //See if we have a new top score
        return (PlayerPrefs.GetInt("High Score 9") < newScore);
    }

    public static void SetCurrentScores(int p1Score, int p2Score)
    {
        //record player one's score for this game
        currentPlayerScores[0] = p1Score;

        //record player two's score for this game
        currentPlayerScores[1] = p2Score;
    }

    public static void NewHighScore(int newScore, char[] newName)
    {
        //Otherwise, let's put that score in
        int i = 0;
        for (i = 9; i >= 0; --i)
        {
            //Starting from the lowest score, we'll see if it's high enough to replace index 'i' on the list
            if (newScore > highScores[i].score)     //if it is...
            {
                //shift all the scores down
                ShiftAllScoresAfter(i);

                //and fill in the newScore at the current index
                highScores[i].name = newName;
                highScores[i].score = newScore;
                break;
            }
        }

        //Save scores to 'permanent' memory
        SaveHighScores();
    }

    //Shift all scores down after the indicated index
    private static void ShiftAllScoresAfter(int index)
    {
        //make score i = i + 1
        for(int i = 9; i > index; i--)
            highScores[i] = highScores[i + 1];
    }

    //Save scores to 'permanent' memory
    private static void SaveHighScores()
    {
        for(int i = 0; i < 10; i++)
        {
            //Save the name of the current scorer at index 'i'
            string name = highScores[i].name.ToString();
            PlayerPrefs.SetString("High Score Name " + i, name);

            //Save the score of the current scorer at index 'i'
            PlayerPrefs.SetInt("High Score " + i, highScores[i].score);
        }
    }

    //Lists of high scores are gathered from long term storage. Not local memory
    public static string GetListOfHighScores()
    {
        string seperator = "........";
        string fullList = "";

        //build string list
        for(int i = 0; i < 10; i++)
        {
            fullList +=
                //Place on the list
                string.Format("00", i + 1) + ". " + PlayerPrefs.GetString("High Score Name " + i)
                //Seperator
                + seperator
                //Actual Score
                + string.Format("000000", PlayerPrefs.GetInt("High Score " + i))
                //next line for new entry in the list
                + '\n';
        }

        return fullList;
    }
}

public struct PlayerScore
{
    public char[] name;
    public int score;

    public PlayerScore(char[] newName, int newScore)
    {
        name = newName;
        score = newScore;
    }
}
