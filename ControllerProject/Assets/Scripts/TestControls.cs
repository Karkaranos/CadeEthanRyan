/*****************************************************************************
// File Name :         TestControls.cs
// Author :            Cade R. Naylor
// Creation Date :     March 5, 2023
//
// Brief Description : Creates test functions for the control test. Handles movement
                        and scope.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControls : MonoBehaviour
{
    //Create an instance of input
    PlayerActions controls;
    Vector2 movement;
    Vector2 scopePos;
    [SerializeField] private GameObject scope;
    private int scopeRange = 100;

    //called just before start
    /*private void Awake()
    {
        controls = new PlayerActions();

        
        //Movement- Left Stick
        //Reads in input from the Left Stick and saves it to a temporary variable
        controls.Player2Actions.Movement.performed += contx => movement =
        contx.ReadValue<Vector2>();
        //When the Left Stick is not being pressed, set the temp variable to 0
        controls.Player2Actions.Movement.canceled += contx => movement = 
        Vector2.zero;


        //Scope Movement- Right Stick
        //Reads in input from the Right Stick and saves it to a temporary variable
        controls.Player2Actions.MoveScope.performed += contx => scopePos =
        contx.ReadValue<Vector2>();
        //When the Right Stick is not being pressed, set the temp variable to 0
        controls.Player2Actions.MoveScope.canceled += contx => scopePos =
        Vector2.zero;
    }

    /// <summary>
    /// Handles player and scope movement
    /// </summary>
    private void FixedUpdate()
    {
        //Create a 
        Vector2 playerPos = transform.position;
        Vector2 newScopePos;
        Vector2 movementVelocity = new Vector2(movement.x, movement.y) * 5 *
            Time.deltaTime;
        float fAngle;
        //Translate is a movement function
        transform.Translate(movementVelocity, Space.Self);

        //Clamp the player's position to stay on screen
        ClampPlayer(transform.position);

        //Set the scope's position to the new value while ensuring it revolves
        //around the player

        fAngle = Mathf.Tan(scopePos.y / scopePos.x);

        newScopePos.x = playerPos.x + (scopePos.x * scopeRange * Time.deltaTime);
        newScopePos.y = playerPos.y + (scopePos.y * scopeRange * Time.deltaTime);

        scope.transform.position = newScopePos;
    }


    /// <summary>
    /// Clamps the player's position to remain onscreen
    /// </summary>
    /// <param name="pos">The player's position</param>
    private void ClampPlayer(Vector2 pos)
    {
        Vector2 playerBind = pos;

        if (pos.x > 8.4f)
        {
            playerBind.x = 8.4f;
        }
        if (pos.x < -8.4f)
        {
            playerBind.x = -8.4f;
        }
        if (pos.y > 4.5f)
        {
            playerBind.y = 4.5f;
        }
        if (pos.y < -4.5f)
        {
            playerBind.y = -4.5f;
        }
        transform.position = playerBind;
    }

    private void OnEnable()
    {
        //Turn on Action Maps; Implicitly called
        controls.Player1Actions.Enable();
    }

    private void OnDisable()
    {
        //Turn off action maps
        controls.Player1Actions.Disable();
    }*/

}
