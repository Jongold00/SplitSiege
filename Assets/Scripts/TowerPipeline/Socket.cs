using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class Socket : MonoBehaviour
{
    private TowerDataSO currentlyPlacedTower;
    public TowerDataSO CurrentlyPlacedTower { get => currentlyPlacedTower; private set => currentlyPlacedTower = value; }
    private GameObject currentlyPlacedTowerObj;
    public GameObject CurrentlyPlacedTowerObj { get => currentlyPlacedTowerObj; private set => currentlyPlacedTowerObj = value; }

    public static event Action<GameObject> OnSocketSelected;
    public static Socket SocketSelected;
    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (SocketSelected != this || !BuildTowerPopupMenu.instance.PopupMenuObj.activeInHierarchy)
        {
            SocketSelected?.EnableBoxCollider();
            SocketSelected = this;
            DisableBoxCollider();
            OnSocketSelected?.Invoke(gameObject);
        }
    }

    public GameObject AddTowerToSocket(TowerDataSO objToSpawn)
    {
        EventsManager.instance.TowerBuilt(objToSpawn);

        GameObject obj = Instantiate(objToSpawn.prefab, transform.position, objToSpawn.prefab.transform.rotation);
        Vector3 newPos = obj.transform.position;
        obj.transform.position = newPos;

        CurrentlyPlacedTower = objToSpawn;
        CurrentlyPlacedTowerObj = obj;

        obj.GetComponentInChildren<TowerBehavior>().SocketTowerIsPlacedOn = this;

        gameObject.SetActive(false);

        return obj;
    }

    public void RemoveTowerFromSocket()
    {
        Destroy(CurrentlyPlacedTowerObj);
        CurrentlyPlacedTowerObj = null;
        CurrentlyPlacedTower = null;
        gameObject.SetActive(true);
        EnableBoxCollider();
    }

    public void DisableBoxCollider()
    {
        boxCollider.enabled = false;
    }

    public void EnableBoxCollider()
    {
        boxCollider.enabled = true;
    }
}
