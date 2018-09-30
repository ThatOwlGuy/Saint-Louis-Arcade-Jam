using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenManagement : MonoBehaviour {

    //Screens
    private Animation gameOverScreen;
    private Animation highScoreScreen;
    private Animation scoreInputScreen;

    private bool inGameOver;
    private bool inHighScore;
    private bool inScoreInput;

    //Gets the animation components of the different screens
    private void Start()
    {
        gameOverScreen = transform.GetChild(0).GetComponent<Animation>();
        highScoreScreen = transform.GetChild(1).GetComponent<Animation>();
        scoreInputScreen = transform.GetChild(2).GetComponent<Animation>();
    }

    public void ToGameOverScreen()
    {
        if (inHighScore) {
            inHighScore = false;
            ExitScreen(highScoreScreen);
        }
        else if (inScoreInput)
        {
            inScoreInput = false;
            ExitScreen(scoreInputScreen);
        }

        inGameOver = true;
        EnterScreen(gameOverScreen);
    }

    public void ToHighScoreScreen()
    {
        if (inGameOver)
        {
            inGameOver = false;
            ExitScreen(gameOverScreen);
        }
        else if (inScoreInput)
        {
            inScoreInput = false;
            ExitScreen(scoreInputScreen);
        }

        inHighScore = true;
        EnterScreen(highScoreScreen);
    }

    public void ToScoreInput()
    {
        if (inHighScore)
        {
            inHighScore = false;
            ExitScreen(highScoreScreen);
        }
        else if (inGameOver)
        {
            inGameOver = false;
            ExitScreen(gameOverScreen);
        }

        inScoreInput = true;
        EnterScreen(scoreInputScreen);
    }

    public void ExitScreen(Animation screen)
    {
        screen.PlayQueued("Exit", QueueMode.PlayNow);
    }

    private void EnterScreen(Animation screen)
    {
        screen.PlayQueued("Enter", QueueMode.PlayNow);
    }
}
