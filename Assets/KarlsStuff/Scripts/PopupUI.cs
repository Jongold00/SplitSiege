using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PopupUI : MonoBehaviour
{
    public static Action OnPopupDisplayed;

    protected RectTransform CalculateRectTransToFitScreen(RectTransform rectTransform)
    {
        Vector2 currentPos = rectTransform.anchoredPosition;

        if ((rectTransform.anchoredPosition.x - (rectTransform.sizeDelta.x / 2)) < 0)
        {
            currentPos.x = rectTransform.sizeDelta.x / 2;
        }

        if ((rectTransform.anchoredPosition.x + (rectTransform.sizeDelta.x / 2)) > Screen.width)
        {
            currentPos.x = Screen.width - (rectTransform.sizeDelta.x / 2);
        }

        if ((rectTransform.anchoredPosition.y - (rectTransform.sizeDelta.y / 2)) < 0)
        {
            currentPos.y = Screen.height + (rectTransform.sizeDelta.y / 2);
        }

        if ((rectTransform.anchoredPosition.y + (rectTransform.sizeDelta.y / 2)) > Screen.height)
        {
            currentPos.y = Screen.height - (rectTransform.sizeDelta.y / 2);
        }

        rectTransform.anchoredPosition = currentPos;

        return rectTransform;
    }
}
