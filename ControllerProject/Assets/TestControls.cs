/*****************************************************************************
// File Name :         TestControls.cs
// Author :            Cade R. Naylor
// Creation Date :     March 5, 2023
//
// Brief Description : Creates test functions for the control test
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
    [SerializeField] private int scopeRange = 100;

    //called just before start
    private void Awake()
    {
        controls = new PlayerActions();

        
        //Movement- Right Stick. Should always be active

        controls.Player1Actions.Movement.performed += contx => movement =
        contx.ReadValue<Vector2>();
        controls.Player1Actions.Movement.canceled += contx => movement = 
        Vector2.zero;


        //Scope Movement- Left Stick. Can swap with Camera Movement 

        controls.Player1Actions.MoveScope.performed += contx => scopePos =
        contx.ReadValue<Vector2>();
        controls.Player1Actions.MoveScope.canceled += contx => scopePos =
        Vector2.zero;
    }

    /// <summary>
    /// Handles player and scope movement
    /// </summary>
    private void FixedUpdate()
    {
        Vector2 playerPos = transform.position;
        Vector2 newScopePos;
        Vector2 movementVelocity = new Vector2(movement.x, movement.y) * 5 *
            Time.deltaTime;
        Vector2 playerBind;
        //Translate is a movement function
        transform.Translate(movementVelocity, Space.Self);
        playerBind = transform.position;
        if (transform.position.x > 8.4f)
        {
            playerBind.x = 8.4f;
        }
        if (transform.position.x < -8.4f)
        {
            playerBind.x = -8.4f;
        }
        if (transform.position.y > 4.5f)
        {
            playerBind.y = 4.5f;
        }
        if (transform.position.y < -4.5f)
        {
            playerBind.y = -4.5f;
        }
        transform.position = playerBind;


        newScopePos.x = playerPos.x+(scopePos.x * scopeRange * Time.deltaTime);
        newScopePos.y = playerPos.y + (scopePos.y * scopeRange * Time.deltaTime);

        scope.transform.position = newScopePos;
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
    }

}
