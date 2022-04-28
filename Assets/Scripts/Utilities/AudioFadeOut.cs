using UnityEngine;
using System.Collections;

public static class AudioFadeOut
{

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {

        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.1f * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = 0;
    }

}

public static class AudioFadeIn
{

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime, float maxVolume)
    {
        audioSource.volume = 0;

        while (audioSource.volume < maxVolume)
        {
            audioSource.volume += 0.1f * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = maxVolume;
    }

}
