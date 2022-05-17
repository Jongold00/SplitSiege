using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMOD_PlayOneShot : MonoBehaviour
{
    public EventReference Event;

    [SerializeField]
    float volume;
    public void Play()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Event);
    }
}
