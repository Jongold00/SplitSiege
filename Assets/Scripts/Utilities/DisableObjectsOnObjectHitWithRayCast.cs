using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using System;

public class DisableObjectsOnObjectHitWithRayCast : MonoBehaviour
{
    [SerializeField] GameObject[] objsToDisable;
    [SerializeField] int[] layerIndexOfLayersToIgnore;
    [SerializeField] string nameOfTagOnClickObject;
    public static event Action OnObjectClicked;

    RaycastHit[] hits = null;
    private MeshCollider meshCollider;

    private void Awake()
    {
        meshCollider = gameObject.GetComponent<MeshCollider>();
    }

    private void OnEnable()
    {
        PopupUI.OnPopupDisplayed += EnableAutoHide;
    }

    private void OnDisable()
    {
        PopupUI.OnPopupDisplayed -= EnableAutoHide;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            hits = Physics.RaycastAll(ray);

            bool shouldReturn = hits.Any((h) => layerIndexOfLayersToIgnore.Contains(h.transform.gameObject.layer));

            if (shouldReturn)
            {
                return;
            }

            OnObjectClicked?.Invoke();
            SetAllObjsToInactive();

        }
    }
    public void SetAllObjsToInactive()
    {
        foreach (GameObject item in objsToDisable)
        {
            item.SetActive(false);
        }
    }

    public void EnableAutoHide(GameObject obj)
    {
        meshCollider.enabled = false;
        Invoke("ActivateObject", 0.1f);
    }

    private void ActivateObject()
    {
        meshCollider.enabled = true;
    }
}
