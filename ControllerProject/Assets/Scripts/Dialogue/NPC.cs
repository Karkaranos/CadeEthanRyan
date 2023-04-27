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
    [SerializeField] private bool objectInteractable = false;
    private bool interacting = false;
    private bool controlMenuActive;

    public bool Interacting { get => interacting; set => interacting = value; }

    [SerializeField] private GameObject player;

    public Dialogue dialogue;

    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
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
        if (collision.gameObject.CompareTag("NPC"))
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
