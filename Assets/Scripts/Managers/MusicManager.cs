using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
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
    // Start is called before the first frame update
    void Start()
    {
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


}
