using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class ResourceManager : MonoBehaviour
{

    #region Singleton

    public static ResourceManager instance;

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


    #endregion Singleton


    public int resources;

    [SerializeField]
    private int PassiveResourceGain;

    [SerializeField]
    private int PassiveResourceRate;

    public int startingResources = 500;

    bool passiveGainOn = false;


    Action<GameStateManager.GameState> onGameStateChanged;

    private void Start()
    {
        onGameStateChanged += OnGameStateChange;
        EventsManager.instance.SubscribeGameStateChange(onGameStateChanged);

        UpdateResources(startingResources);
        InvokeRepeating("PassiveUpdate", 1, PassiveResourceRate);
    }

    private void OnDestroy()
    {
        EventsManager.instance.UnSubscribeGameStateChange(onGameStateChanged);
    }


    public void UpdateResources(int delta)
    {
        resources += delta;
        EventsManager.instance.ResourcesUpdated(delta);
    }

    public bool CheckLegalTranscation(int cost) 
    {
        return cost <= resources;
    }

    public void PassiveUpdate()
    {
        if (passiveGainOn)
        {
            UpdateResources(PassiveResourceGain);
        }
    }

    void OnGameStateChange(GameStateManager.GameState newState)
    {
        if (newState == GameStateManager.GameState.Fighting)
        {
            passiveGainOn = true;
        }
        else
        {
            passiveGainOn = false;
        }
    }


    
}
