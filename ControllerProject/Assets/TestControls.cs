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

        //Translate is a movement function
        transform.Translate(movementVelocity, Space.Self);

        newScopePos = new Vector2(scopePos.x, scopePos.y) * 5 * Time.deltaTime;

        scope.transform.Translate(newScopePos, Space.Self);
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
