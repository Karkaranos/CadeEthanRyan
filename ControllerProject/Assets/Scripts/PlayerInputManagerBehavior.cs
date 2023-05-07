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
    #region Variables
    private PlayerInputManager inputManager;
    [SerializeField] 
    private GameObject sheriff;
    [SerializeField]
    private GameObject bandit;
    [SerializeField]
    private GameObject playerPrefabB;
    #endregion

    #region Functions

    /// <summary>
    /// Sets the input Manager reference
    /// </summary>
    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();

    }

    /// <summary>
    /// Switches prefab on player joining
    /// </summary>
    /// <param name="input">New input</param>
    void OnPlayerJoined(PlayerInput input)
    {
        if (sheriff == null)
        {
            inputManager.playerPrefab = bandit;
        }
        else
        {
            print("bandito");
            inputManager.playerPrefab = bandit;
        }
    }
    #endregion
}
