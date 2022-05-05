using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0,1)]
    public float value = 1;


    [SerializeField]
    Slider sliderLeft;

    [SerializeField]
    Slider sliderRight;

    [SerializeField]
    Image fillImageLeft;

    [SerializeField]
    Image fillImageRight;

    [SerializeField]
    Color fullHP;

    [SerializeField]
    Color noHP;

    float lerpValue;

    // Update is called once per frame
    void Update()
    {
        //print(value);
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);


        lerpValue = value * value;

        fillImageLeft.color = Color.LerpUnclamped(noHP, fullHP, lerpValue);
        sliderLeft.value = value;

        fillImageRight.color = Color.LerpUnclamped(noHP, fullHP, lerpValue);
        sliderRight.value = value;
    }
}
