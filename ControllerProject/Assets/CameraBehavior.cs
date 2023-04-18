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
    /// Start is called before the first frame update. Sets Wave 1 to spawn
    /// </summary>
    void Start()
    {
        StartCoroutine(CheckForPlayers());
    }

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
