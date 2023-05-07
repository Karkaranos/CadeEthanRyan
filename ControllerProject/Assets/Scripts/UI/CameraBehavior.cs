/*****************************************************************************
// File Name :         CameraBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 18, 2023
//
// Brief Description : Sets the camera's position to follow the first player
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    #region Variables
    GameObject player1Obj;
    bool followPlayer=false;
    Vector3 newPos;
    GameObject player2Obj;

    #endregion

    #region Functions

    /// <summary>
    /// Start is called before the first frame update. Checks for players
    /// </summary>
    void Start()
    {
        StartCoroutine(CheckForPlayers());
    }

    /// <summary>
    /// Checks if there is at least one player
    /// </summary>
    /// <returns>How long before check</returns>
    IEnumerator CheckForPlayers()
    {
        for (; ; )
        {
            //If player 1 is null, try finding it
            if (player1Obj == null)
            {
                player1Obj = GameObject.Find("Grayboxed Sheriff(Clone)");
            }

            //If Player 1 is found, start following the player
            if (player1Obj != null)
            {
                followPlayer = true;
            }

            //If player 2 is null, try finding it
            if (player2Obj == null)
            {
                player2Obj = GameObject.Find("Grayboxed Bandit(Clone)");
            }
            yield return new WaitForSeconds(1);
        }
    }


    /// <summary>
    /// Occurs every frame; Sets the camera to follow a player
    /// </summary>
    private void Update()
    {
        //If set to follow a player and Player 1, but not Player 2, exists, follow
        //Player 1
        if (followPlayer&&player2Obj==null&&player1Obj!=null)
        {
            newPos = player1Obj.transform.position;
            newPos.z = -10;
            transform.position = newPos;
        }

        //If set to follow a player and both players exist, follow Player 1
        if (followPlayer && player2Obj != null && player1Obj!=null)
        {
            newPos = player1Obj.transform.position;
            newPos.z = -10;
            transform.position = newPos;
        }

        //If set to follow a player and Player 2, but not Player 1, exists, follow
        //Player 1
        if (followPlayer && player2Obj!=null &&player1Obj==null)
        {
            newPos = player2Obj.transform.position;
            newPos.z = -10;
            transform.position = newPos;
        }
    }
    #endregion
}
