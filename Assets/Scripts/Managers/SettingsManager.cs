using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{

    #region Singleton

    public static SettingsManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    #endregion Singleton
    public GameSettings settings;


    public void OnEnable()
    {
        settings = new GameSettings();
    }

    public void SaveSettings(GameSettings set)
    {
        settings.masterVolume = set.masterVolume;
        settings.musicVolume = set.musicVolume;
        settings.SFXVolume = set.SFXVolume;
        settings.shadows = set.shadows;
        settings.particleEffects = set.particleEffects;
        settings.renderFoliage = set.renderFoliage;
    }
}
