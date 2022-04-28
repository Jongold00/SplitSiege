using System.Collections;
using System.Collections.Generic;

public class GameSettings
{
    public int masterVolume;
    public int musicVolume;
    public int SFXVolume;


    public bool shadows;
    public bool particleEffects;
    public bool renderFoliage;

    public GameSettings()
    {
        masterVolume = 50;
        musicVolume = 50;
        SFXVolume = 50;

        shadows = true;
        particleEffects = true;
        renderFoliage = true;
    }

}
