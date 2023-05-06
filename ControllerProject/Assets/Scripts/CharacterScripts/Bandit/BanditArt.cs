/*****************************************************************************
// File Name :         BanditArt.cs
// Author :            Cade R. Naylor
// Creation Date :     April 8, 2023
//
// Brief Description : Changes the Bandit's walk animation
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditArt : MonoBehaviour
{
    public void SetDirection(Vector2 pos, Quaternion rot)
    {
        if (pos.x > 0.05 && pos.x < 1)
        {
            //GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }
        else if (pos.x < -0.05 && pos.x > -1)
        {
            //GetComponent<Renderer>().material.color = new Color(0, 255, 255);
        }
        else if (pos.y > 0 && pos.y < 1)
        {
            //GetComponent<Renderer>().material.color = new Color(0, 0, 255);
        }
        else if (pos.y < 0 && pos.y > -1)
        {
            //GetComponent<Renderer>().material.color = new Color(255, 0, 255);
        }

    }
}
