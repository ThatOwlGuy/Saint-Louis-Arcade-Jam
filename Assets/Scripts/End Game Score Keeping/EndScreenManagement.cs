using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHelper))]
public class EndScreenManagement : MonoBehaviour {

    private InputHelper input;

    public GameObject gameOver;
    public GameObject currentHighScore;
    public GameObject newHighScore;
    public GameObject highScoreList;

    private int screenIndex = -1;

    public bool screenTaskComplete;

    public void Start()
    {
        NextScreen();
    }

    public void Update()
    {
        if (!screenTaskComplete)
            return;

        if (PressedAnyButton())
            NextScreen();
    }

    private void NextScreen()
    {
        GameObject newScreen = null;

        switch (screenIndex)
        {
            case 0:
                newScreen = currentHighScore;
                break;
            case 1:
                if (ScoreKeeping.IsNewHighScore(ScoreKeeping.currentPlayerScores[0]))
                    newScreen = newHighScore;
                else
                    goto case 2;
                break;                
            case 2:
                break;
            case 3:
                newScreen = highScoreList;
                break;
        }

        if (newScreen == null)
            return;

        LoadScreen(newScreen);
    }

    private void LoadScreen(GameObject newScreen)
    {
        //Destroy the old screen
        Destroy(transform.GetChild(0).gameObject);

        //Instantiate a new Screen
        GameObject screen;
        screen = Instantiate(newScreen, transform.position, Quaternion.identity);

        //Make the screen renderable by making it a child of the canvas
        screen.transform.SetParent(this.transform);

        screenTaskComplete = false;
    }

    //Buttons should be set to the directional keys in the helper
    private bool PressedAnyButton()
    {
        return (Input.GetKey(input.up) || Input.GetKey(input.left) || Input.GetKey(input.right) || Input.GetKey(input.down));
    }
}
