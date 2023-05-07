/*****************************************************************************
// File Name :         SheriffArt.cs
// Author :            Cade R. Naylor
// Creation Date :     March 30, 2023
//
// Brief Description : Changes the Sheriff's walk animation
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffArt : MonoBehaviour
{
    /// <summary>
    /// Detects what direction the Sheriff is moving in and changes its color
    /// </summary>
    /// <param name="pos">Sheriff's position change</param>
    /// <param name="rot">Sheriff's rotation</param>
    public void SetDirection(Vector2 pos, Quaternion rot)
    {
        if (pos.x > 0.05 && pos.x < 1)
        {
            //GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }
        else if (pos.x < -0.05 && pos.x > -1)
        {
            //GetComponent<Renderer>().material.color = new Color(255, 255, 0);
        }
        else if (pos.y > 0 && pos.y < 1)
        {
            //GetComponent<Renderer>().material.color = new Color(0, 0, 255);
        }
        else if (pos.y < 0 && pos.y > -1)
        {
            //GetComponent<Renderer>().material.color = new Color(0, 255, 255);
        }

    }
}
