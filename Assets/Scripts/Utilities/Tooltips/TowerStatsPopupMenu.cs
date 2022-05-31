using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStatsPopupMenu : MonoBehaviour
{
    // This script should be placed on a canvas object,
    // inside of which is the popup menu itself and related buttons/images


    [SerializeField]
    private GameObject popupMenuObj;
    public GameObject PopupMenuObj { get => popupMenuObj; private set => popupMenuObj = value; }

    private RectTransform rectTransformOfPopupMenu;

    #region Singleton

    public static TowerStatsPopupMenu instance;


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

    private void Start()
    {
        rectTransformOfPopupMenu = PopupMenuObj.GetComponent<RectTransform>();
    }


    public void DisplayPopupMenuAtViewportOfObj(GameObject obj)
    {
        Vector2 offset = new Vector2(0, 80f);
        PopupMenuObj.SetActive(true);

        Vector2 viewportPoint = Camera.main.WorldToScreenPoint(obj.transform.position);
        rectTransformOfPopupMenu.anchoredPosition = viewportPoint + offset;
    }

    public void HidePopupMenu(GameObject obj)
    {
        PopupMenuObj.SetActive(false);
    }
}
