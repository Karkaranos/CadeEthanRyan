/*****************************************************************************
// File Name :         Sound.cs
// Author :            Ethan S. Reising
// Creation Date :     April 20, 2023
//
// Brief Description : Stores sounds and music
*****************************************************************************/
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    //these are variables relating to the name, volume, and pitch of an audio clip
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
