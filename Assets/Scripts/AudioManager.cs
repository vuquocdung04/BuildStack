using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : LoadAuto
{
    [SerializeField] AudioClip clipBreak;
    [SerializeField] AudioClip clipPerfect;
    [SerializeField] AudioSource audioSource;

    public void SoundBrick()
    {
        audioSource.PlayOneShot(clipBreak);
    }

    public void SoundPerFect()
    {
        audioSource.PlayOneShot(clipPerfect);
    }
}
