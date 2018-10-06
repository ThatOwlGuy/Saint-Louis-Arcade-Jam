using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHelper))]
public class LoadSceneOnButtonPress : MonoBehaviour {

    private InputHelper input;
    public string sceneName;

    private void Start()
    {
        input = GetComponent<InputHelper>();
    }

    // Update is called once per frame
    private void Update ()
    {
        if (PressedAnyButton())
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
	}

    //Buttons should be set to the directional keys in the helper
    private bool PressedAnyButton()
    {
        return (Input.GetKey(input.up) || Input.GetKey(input.left) || Input.GetKey(input.right) || Input.GetKey(input.down));
    }
}
