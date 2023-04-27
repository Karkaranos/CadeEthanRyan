/*****************************************************************************
// File Name :         AudioManager.cs
// Author :            Ethan S. Reising
// Creation Date :     April 20, 2023
//
// Brief Description : Manages Audio
*****************************************************************************/
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    /// <summary>
    /// this awake function defines the source of an audio clip or music
    /// and defines the volume and pitch of the audio
    /// </summary>
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Theme");
    }

    /// <summary>
    /// Allows you to easily access and play sounds
    /// </summary>
    /// <param name="name">the name of the sound or music</param>
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
