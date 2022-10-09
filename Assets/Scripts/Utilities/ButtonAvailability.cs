using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAvailability : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private float alphaOfButtonWhenUnavailable;
    private bool isButtonAvailable;
    public bool IsButtonAvailable { get; private set; }
    private Color startingColor;

    private void Awake()
    {
        startingColor = image.color;
    }
    public void MakeButtonUnavailable()
    {
        button.interactable = false;
        Color newColor = image.color;
        newColor.a = alphaOfButtonWhenUnavailable;
        image.color = newColor;
        IsButtonAvailable = false;
    }

    public void MakeButtonAvailable()
    {
        button.interactable = true;
        image.color = startingColor;
        IsButtonAvailable = true;
    }
}
