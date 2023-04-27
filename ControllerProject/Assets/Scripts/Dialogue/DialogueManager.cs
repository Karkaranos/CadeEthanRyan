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
    [SerializeField] private bool objectInteractable = false;
    private bool interacting = false;
    private bool controlMenuActive;

    public bool Interacting { get => interacting; set => interacting = value; }

    [SerializeField] private GameObject player;


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
        if (objectInteractable && !controlMenuActive)
        {
            Interacting = true;
        }
    }
    
    /// <summary>
    /// Allows player to interact when near an object.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Only allows players to interact with object tagged properly.
        if (collision.gameObject.CompareTag("Interactable"))
        {
            objectInteractable = true;
        }
    }

    /// <summary>
    /// Disallows player to interact when not near an object.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (objectInteractable)
        {
            objectInteractable = false;
        }
    }

}
