using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHelper))]
public class EndScreenManagement : MonoBehaviour {

    private InputHelper input;

    public GameObject gameOver;
    public GameObject currentHighScore;

    [SerializeField]
    private int screenIndex = 0;

    public bool screenTaskComplete = false;

    public void Start()
    {
        input = GetComponent<InputHelper>();
        --screenIndex;
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
    }

    //Buttons should be set to the directional keys in the helper
    private bool PressedAnyButton()
    {
        return (Input.GetKeyDown(input.up) || Input.GetKeyDown(input.left) || Input.GetKeyDown(input.right) || Input.GetKeyDown(input.down));
    }
}
