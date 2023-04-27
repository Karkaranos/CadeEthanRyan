/*****************************************************************************
// File Name :         PlayerInputManagerBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 18, 2023
//
// Brief Description : Allows for multiple unique characters
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManagerBehavior : MonoBehaviour
{
    private PlayerInputManager inputManager;
    [SerializeField] 
    private GameObject sheriff;
    [SerializeField]
    private GameObject bandit;
    [SerializeField]
    private GameObject playerPrefabB;

    /// <summary>
    /// Sets the input Manager
    /// </summary>
    private void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }

    /// <summary>
    /// Switches prefab ob player joining
    /// </summary>
    /// <param name="input">New input</param>
    void OnPlayerJoined(PlayerInput input)
    {
        if (sheriff == null)
        {
            //sheriff = input.gameObject;
            inputManager.playerPrefab = bandit;
        }
        else
        {
            print("bandito");
            //bandit = input.gameObject;
            inputManager.playerPrefab = bandit;
        }
    }
}
