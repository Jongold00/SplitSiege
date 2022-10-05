using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectivePanAndZoom : MonoBehaviour
{
    public float panScaleFactor = 1.0f;
    public float zoomScaleFactor = 0.01f;
    public float groundY = 0.0f;

    Vector3 panStart;
    Camera cam;
    Camera[] childCams;

    Vector2 lowerBounds;
    Vector2 upperBounds;

    float zoomInBound;
    float zoomOutBound;


    public float currentIncrease = 0.0f;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        childCams = GetComponentsInChildren<Camera>();
        lowerBounds = new Vector2(transform.position.x, transform.position.z);
        upperBounds = new Vector2(transform.position.x, transform.position.z);

        zoomInBound = cam.fieldOfView / 2;
        zoomOutBound = cam.fieldOfView;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            panStart = GetPerspectiveWorldPos(groundY);

        }

        if (Input.touchCount == 2)
        {
            Touch touchOne = Input.touches[0];
            Touch touchTwo = Input.touches[1];

            Vector2 prevTouchOne = touchOne.position - touchOne.deltaPosition;
            Vector2 prevTouchTwo = touchTwo.position - touchTwo.deltaPosition;

            float previousMagnitude = (prevTouchOne - prevTouchTwo).magnitude;
            float currentMagnitude = (touchOne.position - touchTwo.position).magnitude;

            float difference = previousMagnitude - currentMagnitude;

            Zoom(difference * zoomScaleFactor);
        }

        else if (Input.GetMouseButton(0))
        {
            Vector3 currentPanPosition = GetPerspectiveWorldPos(groundY);
            Vector3 panOffset = panStart - currentPanPosition;
            //transform.position += panOffset * panScaleFactor; 

            float clampedX = Mathf.Clamp(transform.position.x + (panOffset.x * panScaleFactor), lowerBounds.x, upperBounds.x);
            float clampedZ = Mathf.Clamp(transform.position.z + (panOffset.z * panScaleFactor), lowerBounds.y, upperBounds.y);


            transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
        }

    }


    public void ExposedPinchMock(bool zoomIn)
    {
        Vector2 spot1 = new Vector2(1.0f, 5.1f);
        Vector2 spot2 = new Vector2(-0.7f, 1.6f);
        currentIncrease += 1f;

        PinchMock(spot1, spot2, zoomIn, currentIncrease);
    }

    private void PinchMock(Vector2 pos1, Vector2 pos2, bool increase, float scaleFactor)
    {
        float xGap = (pos1.x - pos2.x) / (scaleFactor * (increase ? 1 : -1));
        float yGap = (pos1.y - pos2.y) / (scaleFactor * (increase ? 1 : -1));

        Vector2 additionVec = new Vector2(xGap, yGap);

        Vector2 prevTouchTwo = pos2 + additionVec;


        float previousMagnitude = (pos1 - prevTouchTwo).magnitude;
        float currentMagnitude = (pos1 - pos2).magnitude;

        print(previousMagnitude);


        float difference = previousMagnitude - currentMagnitude;
        Zoom(difference * zoomScaleFactor);
    }


    private Vector3 GetPerspectiveWorldPos(float y)
    {
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0, y, 0));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }


    private void Zoom(float delta)
    {
        float prevFOV = cam.fieldOfView;

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView + delta, zoomInBound, zoomOutBound);

        float actualDelta = cam.fieldOfView - prevFOV;

        foreach (Camera curr in childCams)
        {
            curr.fieldOfView = cam.fieldOfView;
        }



        lowerBounds = new Vector2(lowerBounds.x + (actualDelta / 2), lowerBounds.y + (actualDelta / 2));
        upperBounds = new Vector2(upperBounds.x - (actualDelta / 2), upperBounds.y - (actualDelta / 2));

        print(lowerBounds);
        print(upperBounds);


        //print(cam.fieldOfView);
    }
}