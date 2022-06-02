using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControlSlider : MonoBehaviour
{
    // Update is called once per frame
    public void OnValueChangeCheck()
    {
        EventsManager.instance.MusicVolumeChange(GetComponent<Slider>().value);
    }



}
