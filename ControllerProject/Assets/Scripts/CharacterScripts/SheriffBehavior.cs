using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffBehavior : MonoBehaviour
{//Create an instance of input
    PlayerActions controls;
    Vector2 movement;
    Vector2 scopePos;
    [SerializeField] private GameObject scope;
    private int scopeRange = 100;
    [SerializeField] private WeaponData weapon;

    //called just before start
    private void Awake()
    {
        controls = new PlayerActions();


        //Movement - Left Stick
        //Reads in input from the Left Stick and saves it to a temporary variable
        controls.Player1Actions.Movement.performed += contx => movement =
        contx.ReadValue<Vector2>();
        //When the Left Stick is not being pressed, set the temp variable to 0
        controls.Player1Actions.Movement.canceled += contx => movement =
        Vector2.zero;


        //Scope Movement - Right Stick
        //Reads in input from the Right Stick and saves it to a temporary variable
        controls.Player1Actions.MoveScope.performed += contx => scopePos =
        contx.ReadValue<Vector2>();
        //When the Right Stick is not being pressed, set the temp variable to 0
        controls.Player1Actions.MoveScope.canceled += contx => scopePos =
        Vector2.zero;

        //Weapon Switching - Left Trigger
        controls.Player1Actions.SwitchWeapon.performed += contx => SwitchWeapon();

        //Quick Attack - A button
        controls.Player1Actions.QuickAttack.performed += contx => quickAtk();

        //Charged Attack - B Button
        controls.Player1Actions.ImpactAttack.performed += contx => chargeAtk();
    }

    private void chargeAtk()
    {
        print(weapon.Weapon + " deals " + weapon.ChargeDmg + " damage.");
        //Set a cooldown timer until this attack can be used again
    }

    private void quickAtk()
    {
        print(weapon.Weapon + " deals " + weapon.Dmg + " damage.");
    }

    /// <summary>
    /// Switches the WeaponData the player is currently using
    /// </summary>
    private void SwitchWeapon()
    {
        string fileName = "";
        if (weapon.Weapon == WeaponData.WeaponID.REVOLVER)
        {
            fileName = "SHOTGUN_DATA";
            print("Weapon switched to Shotgun");
        }
        else if (weapon.Weapon == WeaponData.WeaponID.SHOTGUN)
        {

            fileName = "REVOLVER_DATA";
            print("Weapon switched to Revolver");
            //fileName = "PISTOL_DATA";
            //print("Weapon switched to Pistol");
        }
        /*else
        {
            fileName = "REVOLVER_DATA";
        }*/
        weapon = Resources.Load<WeaponData>(fileName);
    }

    /// <summary>
    /// Handles player and scope movement
    /// </summary>
    private void FixedUpdate()
    {
        //Create a reference to the player's position
        Vector2 playerPos = transform.position;
        Vector2 newScopePos;
        Vector2 movementVelocity = new Vector2(movement.x, movement.y) * 5 *
            Time.deltaTime;
        float fAngle;
        float scopeDistance;
        Quaternion playerRot = transform.rotation;

        //Translate is a movement function
        transform.Translate(movementVelocity, Space.Self);

        //Clamp the player's position to stay on screen
        ClampPlayer(transform.position);

        //Set the scope's position to the new value while ensuring it revolves
        //around the player

        fAngle = Mathf.Atan(scopePos.y / scopePos.x);
        scopeDistance = Mathf.Sqrt((Mathf.Pow(scopePos.x, 2)) + 
            (Mathf.Pow(scopePos.y, 2)));


        newScopePos.x = playerPos.x + (scopePos.x * scopeDistance * scopeRange * 
            Time.deltaTime);
        newScopePos.y = playerPos.y + (scopePos.y * scopeDistance * scopeRange *
            Time.deltaTime);

        scope.transform.position = newScopePos;


        if (movementVelocity.x > 0.05 && movementVelocity.x < 1)
        {
            GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            playerRot.z = 0f;
            
        }
        else if (movementVelocity.x < -0.05 && movementVelocity.x > -1)
        {
            GetComponent<Renderer>().material.color = new Color(255, 255, 0);
            playerRot.z = 180f;
        }
        else  if (movementVelocity.y > 0 && movementVelocity.y < 1)
        {
            GetComponent<Renderer>().material.color = new Color(0, 0, 255);
            playerRot.z = 90f;
        }
        else if (movementVelocity.y < 0 && movementVelocity. y > -1)
        {
            GetComponent<Renderer>().material.color = new Color(0, 255, 255);
            playerRot.z = 270f;
        }

        transform.rotation = playerRot;

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
    }

}
