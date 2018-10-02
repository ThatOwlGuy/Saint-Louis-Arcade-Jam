﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeToQuit : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
            Debug.Log("Application has been quit!");
        }
    }
}
