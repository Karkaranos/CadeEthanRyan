/*****************************************************************************
// File Name :         MenuIndicatorBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 12, 2023
//
// Brief Description : Creates a visible object so player can interact with menus
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuIndicatorBehavior : MonoBehaviour
{
    //Create an instance of input
    InputActionAsset inputAsset;
    InputActionMap inputMap;

    InputAction move;
    InputAction click;

    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        inputMap = inputAsset.FindActionMap("MenuActions");
        move = inputMap.FindAction("Move");
        click = inputMap.FindAction("Click");

        //Movement - Left Stick
        //Reads in input from the Left Stick and saves it to a temporary variable
        move.performed += contx => movement = contx.ReadValue<Vector2>();
        //When the Left Stick is not being pressed, set the temp variable to 0
        move.canceled += contx => movement = Vector2.zero;
    }

    private void OnEnable()
    {
        //Turn on Action Maps; Implicitly called
        //inputMap.Enable();
    }

    private void OnDisable()
    {
        //Turn off action maps
        inputMap.Disable();
    }

// Update is called once per frame
void FixedUpdate()
    {
        Vector2 movementVelocity = new Vector2(movement.x, movement.y) * 5 *
            Time.deltaTime;
        transform.Translate(movementVelocity, Space.Self);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Level1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
