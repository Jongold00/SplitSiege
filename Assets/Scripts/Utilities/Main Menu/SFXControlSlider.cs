using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXControlSlider : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnValueChangeCheck()
    {
        EventsManager.instance.SFXVolumeChange(GetComponent<Slider>().value);
    }

}
