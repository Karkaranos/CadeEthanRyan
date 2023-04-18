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

    private void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }

    void OnPlayerJoined(PlayerInput input)
    {
        if (sheriff == null)
        {
            sheriff = input.gameObject;
            inputManager.playerPrefab = playerPrefabB;
        }
        else
        {
            bandit = input.gameObject;
        }
    }
}
