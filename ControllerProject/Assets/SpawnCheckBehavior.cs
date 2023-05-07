/*****************************************************************************
// File Name :         SpawnCheckBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     May 5, 2023
//
// Brief Description : Checks if spawn space is empty and returns a value
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCheckBehavior : MonoBehaviour
{
    #region Variables
    public bool isOverlapping=false;
    #endregion

    //Checks if object is overlapping and set isOverlapping to true if it is
    #region Functions
    /// <summary>
    /// Checks for overlap when entering a trigger
    /// </summary>
    /// <param name="collision">The object triggered by</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOverlapping = true;
    }

    /// <summary>
    /// Checks for overlap when staying in a trigger
    /// </summary>
    /// <param name="collision"> The object triggered by</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        isOverlapping = true;
    }

    #endregion
}
