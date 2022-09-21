using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisableObjectsOnObjectHitWithRayCast : MonoBehaviour
{
    [SerializeField] GameObject[] objsToDisable;
    [SerializeField] int layerIndexOfLayerToIgnore;
    [SerializeField] string nameOfTagOnClickObject;

    List<GameObject> collisionList = new List<GameObject>();
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
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))

                if (hit.transform.gameObject.layer == 6)
                {
                    return;
                }

                if (hit.transform.gameObject.tag == "Background")
                {
                    SetAllObjsAndThisToInactive();
                }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        collisionList.Add(col.gameObject);
    }


    void OnTriggerExit(Collider col)
    {
        collisionList.Remove(col.gameObject);
    }
    public void SetAllObjsAndThisToInactive()
    {
        foreach (GameObject item in objsToDisable)
        {
            item.SetActive(false);
        }
    }

    public void EnableAutoHide()
    {
        meshCollider.enabled = false;
        Invoke("ActivateObject", 0.1f);
    }

    private void ActivateObject()
    {
        meshCollider.enabled = true;
    }
}
