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

    GameObject player1Obj;
    bool followPlayer=false;
    Vector3 newPos;
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
            if (player1Obj == null)
            {
                player1Obj = GameObject.Find("Grayboxed Sheriff(Clone)");
            }
            if (player1Obj != null)
            {
                followPlayer = true;
            }
            yield return new WaitForSeconds(1);
        }
    }


    /// <summary>
    /// Sets the camera to follow player 1
    /// </summary>
    private void Update()
    {
        if (followPlayer)
        {
            newPos = player1Obj.transform.position;
            newPos.z = -10;
            transform.position = newPos;
        }
    }
}
