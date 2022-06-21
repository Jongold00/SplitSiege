using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

public class GlobalSFXManager : MonoBehaviour
{


    #region Singleton

    public static GlobalSFXManager instance;

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

    #region Tower Building

    public EventReference goodTowerPlaced;
    public EventReference evilTowerPlaced;

    public Action<TowerDataSO> onTowerPlaced;

    public void OnTowerPlaced(TowerDataSO newTower)
    {
        if (newTower.faction == 0)
        {
            FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(goodTowerPlaced);
            eventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(MainCameraTransform()));
            eventInstance.start();
        }
        else
        {
            FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(evilTowerPlaced);
            eventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(MainCameraTransform()));

            eventInstance.start();

        }
    }

    #endregion


    Transform MainCameraTransform()
    {
        return Camera.main.transform;
    }

    void Start()
    {


        onTowerPlaced += OnTowerPlaced;
        EventsManager.instance.SubscribeTowerBuilt(onTowerPlaced);
    }

    private void OnDestroy()
    {
        EventsManager.instance.UnsubscribeTowerBuilt(onTowerPlaced);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
