using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
using System;
using PathCreation;

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



    FMOD.Studio.PARAMETER_ID GamePhaseID;
    [Range(0, 3)]
    public int GamePhase;

    FMOD.Studio.PARAMETER_ID NumberOfMobsID;
    [Range(0, 10)]
    public float NumberOfMobs;

    FMOD.Studio.PARAMETER_ID MobProgressID;
    [Range(0, 100)]
    public float MobProgress;

    FMOD.Studio.PARAMETER_ID TowerBalanceID;
    [Range(0, 100)]
    public float TowerBalance;





    Action<TowerDataSO> onTowerBuilt;

    float evilTowersBuilt = 0;
    float totalTowersBuilt = 0;

    Action<GameStateManager.GameState> onGameStateChange;


    Action<float> onVolumeChange;
    public float volume = 0.5f;

    PathCreator path;



    // IMPORTANAT NOTE, BE SURE TO LERP INTENSITY VALUES, NOT SET





    // Start is called before the first frame update
    void Start()
    {

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        FMOD.Studio.EventDescription eventDescription;

        musicInstance.getDescription(out eventDescription);


        FMOD.Studio.PARAMETER_DESCRIPTION gamePhaseDescription;
        eventDescription.getParameterDescriptionByName("Gameplay Status", out gamePhaseDescription);
        GamePhaseID = gamePhaseDescription.id;

        FMOD.Studio.PARAMETER_DESCRIPTION numberOfMobsDescription;
        eventDescription.getParameterDescriptionByName("Number of Mobs", out numberOfMobsDescription);
        NumberOfMobsID = numberOfMobsDescription.id;

        FMOD.Studio.PARAMETER_DESCRIPTION mobProgressDescription;
        eventDescription.getParameterDescriptionByName("Mob Progress", out mobProgressDescription);
        MobProgressID = mobProgressDescription.id;

        FMOD.Studio.PARAMETER_DESCRIPTION towerBalanceDescrtiption;
        eventDescription.getParameterDescriptionByName("Tower Balance", out towerBalanceDescrtiption);
        TowerBalanceID = towerBalanceDescrtiption.id;





        TowerBalance = 50;
        MobProgress = 0;
        GamePhase = 0;
        NumberOfMobs = 0;



        onTowerBuilt += OnTowerBuilt;
        EventsManager.instance.SubscribeTowerBuilt(onTowerBuilt);



        onVolumeChange += SetVolume;
        EventsManager.instance.SubscribeMusicVolumeChange(onVolumeChange);

        onGameStateChange += OnGameStateChange;
        EventsManager.instance.SubscribeGameStateChange(onGameStateChange);


        musicInstance.start();
    }

    private void OnDestroy()
    {
        EventsManager.instance.UnsubscribeTowerBuilt(onTowerBuilt);
        EventsManager.instance.UnSubscribeGameStateChange(onGameStateChange);
        EventsManager.instance.UnSubscribeMusicVolumeChange(onVolumeChange);


    }

    private void Update()
    {
        NumberOfMobs = Mathf.Min(UnitBehavior.allEnemies.Count, 10);
        musicInstance.setParameterByID(NumberOfMobsID, NumberOfMobs);

        MobProgress = Mathf.Lerp(MobProgress, GetHighestProgress(), 0.1f);
        musicInstance.setParameterByID(MobProgressID, MobProgress);

        float throwaway;
        musicInstance.getParameterByID(NumberOfMobsID, out throwaway);

    }

    public void SetVolume(float set)
    {
        volume = set;
    }

    public void OnTowerBuilt(TowerDataSO towerData)
    {
        totalTowersBuilt++;
        if (towerData.faction == 1)
        {
            evilTowersBuilt++;
        }
        TowerBalance = (evilTowersBuilt / totalTowersBuilt) * 100;
        musicInstance.setParameterByID(TowerBalanceID, TowerBalance);
    }

    public void OnGameStateChange(GameStateManager.GameState newState)
    {
        switch(newState)
        {
            case GameStateManager.GameState.Building:
                GamePhase = 0;
                break;
            case GameStateManager.GameState.Fighting:
                GamePhase = 1;
                break;
            case GameStateManager.GameState.Won:
                GamePhase = 2;
                break;
            case GameStateManager.GameState.Lost:
                GamePhase = 3;
                break;
        }
        musicInstance.setParameterByID(GamePhaseID, GamePhase);

    }

    public float GetHighestProgress()
    {
        if (path == null)
        {
            path = FindObjectOfType<PathCreator>();
        }

        float highestSoFar = 0;
        UnitNavigation[] navs = GameObject.FindObjectsOfType<UnitNavigation>();

        foreach (UnitNavigation curr in navs)
        {
            print(curr.GetDistanceTravelled());
            float progress = (curr.GetDistanceTravelled() / path.path.length) * 100;
            highestSoFar = Mathf.Max(highestSoFar, progress);
        }
        return highestSoFar;
    }

}
