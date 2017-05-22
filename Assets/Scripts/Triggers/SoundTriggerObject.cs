using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggerObject : TriggerObject
{
    public AudioClip sound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override bool OnGameTrigger()
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(sound);
        }
        
        return OneTimeExecute;
    }
}
