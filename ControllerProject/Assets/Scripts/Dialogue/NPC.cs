/*****************************************************************************
// File Name :         NPC.cs
// Author :            Ethan S. Reising
// Creation Date :     April 26, 2023
//
// Brief Description : this triggers dialogue with the npc
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

    }
}
