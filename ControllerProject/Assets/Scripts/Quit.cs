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
