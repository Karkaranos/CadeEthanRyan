/*****************************************************************************
// File Name :         DialogueManager.cs
// Author :            Ethan S. Reising
// Creation Date :     April 26, 2023
//
// Brief Description : Manages Dialogue
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //variables realting to dialogue box
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    //a queue for the npc's sentences
    private Queue<string> sentences;

    /// <summary>
    /// assigns sentences to the queue
    /// </summary>
    void Start()
    {
        sentences = new Queue<string>();
    }

    /// <summary>
    /// this opens the dialogue box for the player and displays the next sentence
    /// when continue is pressed
    /// </summary>
    /// <param name="dialogue">the list of dialogue for the npc</param>
    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    /// <summary>
    /// displays the next sentence in the list of sentences and
    /// closes the dialogue box when the player has 
    /// continued through all the sentences
    /// </summary>
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// animates the npc's sentences
    /// </summary>
    /// <param name="sentence">the current sentence in the dialogue box</param>
    /// <returns></returns>
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    /// <summary>
    /// closes the dialogue box 
    /// </summary>
    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        
    }
}
