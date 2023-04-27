/*****************************************************************************
// File Name :         DialogueManager.cs
// Author :            Ethan S. Reising
// Creation Date :     April 26, 2023
//
// Brief Description : Manages Dialogue
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;

    
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);

        sentences.Clear();
    }

    public void Interact()
    {

    }
}
