/*****************************************************************************
// File Name :         Quit.cs
// Author :            Cade R. Naylor
// Creation Date :     April 18, 2023
//
// Brief Description : Quit Manager
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Quit : MonoBehaviour
{
    InputActionAsset inputAsset;
    InputActionMap inputMap;

    InputAction quit;
    // Start is called before the first frame update
    void Start()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        inputMap = inputAsset.FindActionMap("Player1Actions");
        quit = inputMap.FindAction("Quit");

        quit.performed += contx => QuitMe();
    }

    private void QuitMe()
    {
        print("Application quit");
        Application.Quit();
    }

}
