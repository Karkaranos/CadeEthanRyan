/*****************************************************************************
// File Name :         BottleBehaviour.cs
// Author :            Ethan S. Reising
// Creation Date :     May 7, 2023
//
// Brief Description : Script that destroys bottles
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleBehaviour : MonoBehaviour
{
    TutorialManager tM;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        tM = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
        if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "explodey")
        {
            tM.bottles.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    
}
