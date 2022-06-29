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
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(Event);
        eventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
        eventInstance.start();
        print(eventInstance);

    }
    
}
