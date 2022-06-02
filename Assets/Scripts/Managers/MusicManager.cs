using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
using System;
public class MusicManager : MonoBehaviour
{

    #region Singleton

    public static MusicManager instance;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    #endregion

    public EventReference musicEvent;
    FMOD.Studio.EventInstance musicInstance;

    FMOD.Studio.PARAMETER_ID intensityID;
    [Range(0, 5)]
    public float intensity;


    Action<float> onVolumeChange;
    public float volume = 0.5f;

    // IMPORTANAT NOTE, BE SURE TO LERP INTENSITY VALUES, NOT SET





    // Start is called before the first frame update
    void Start()
    {
        onVolumeChange += SetVolume;
        EventsManager.instance.SubscribeMusicVolumeChange(onVolumeChange);

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        FMOD.Studio.EventDescription eventDescription;
        FMOD.Studio.PARAMETER_DESCRIPTION intensityDescription;

        musicInstance.getDescription(out eventDescription);
        eventDescription.getParameterDescriptionByName("Intensity", out intensityDescription);

        intensityID = intensityDescription.id;

        musicInstance.start();
    }

    private void Update()
    {
        musicInstance.setParameterByID(intensityID, intensity);
    }

    public void SetVolume(float set)
    {
        volume = set;
    }


}
