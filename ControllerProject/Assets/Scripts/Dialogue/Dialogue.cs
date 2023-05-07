/*****************************************************************************
// File Name :         Dialogue.cs
// Author :            Ethan S. Reising
// Creation Date :     April 26, 2023
//
// Brief Description : Dialogue Class
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    //name of the npc
    public string name;

    //NPC sentences
    [TextArea(3, 10)]
    public string[] sentences;
}
