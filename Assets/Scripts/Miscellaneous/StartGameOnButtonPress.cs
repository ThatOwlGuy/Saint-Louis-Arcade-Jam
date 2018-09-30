using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameOnButtonPress : MonoBehaviour {

    public InputHelper input;

    public void Update()
    {
        if (ButtonIsPressed())
        {
            SceneManager.LoadScene(1);
        }
    }

    private bool ButtonIsPressed()
    {
        //Only four of the inputs will be taken for the buttons so that
        //the joystick will not trigger a game start
        return Input.GetKey(input.left) | Input.GetKey(input.right) | Input.GetKey(input.up) | Input.GetKey(input.down);
    }
}
