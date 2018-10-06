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

    public bool screenTaskComplete = false;

    public void Start()
    {
        input = GetComponent<InputHelper>();
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
        screenIndex++;

        GameObject newScreen = null;

        switch (screenIndex)
        {
            case 0:
                newScreen = currentHighScore;
                break;
            case 1:
                if (ScoreKeeping.IsNewHighScore(ScoreKeeping.currentPlayerScores[0]) || ScoreKeeping.IsNewHighScore(ScoreKeeping.currentPlayerScores[1]))
                    newScreen = newHighScore;
                else
                    goto case 2;
                break;                
            case 2:
                newScreen = highScoreList;
                break;
            case 3:
                newScreen = gameOver;
                break;
            default:
                //Load start screen if we've exhasted all steps
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                break;
        }

        if (newScreen == null)
            return;

        LoadScreen(newScreen);
    }

    private void LoadScreen(GameObject newScreen)
    {
        //Destroy the old screen
        if(transform.childCount != 0)
            Destroy(transform.GetChild(0).gameObject);

        //Instantiate a new Screen
        RectTransform screen;
        screen = Instantiate(newScreen, transform.position, Quaternion.identity).GetComponent<RectTransform>();

        //Make the screen renderable by making it a child of the canvas
        screen.transform.SetParent(this.transform);

        //Reset offsets to ensure that the screen renders properly
        screen.offsetMin = new Vector2(0f, 0f);
        screen.offsetMax = new Vector2(0f, 0f);

        screenTaskComplete = false;

        print("Loaded screen");
    }

    //Buttons should be set to the directional keys in the helper
    private bool PressedAnyButton()
    {
        return (Input.GetKey(input.up) || Input.GetKey(input.left) || Input.GetKey(input.right) || Input.GetKey(input.down));
    }
}
