﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Camera cam;
    
    float sensitivity;
    
    float[] oldMouseCoords;
    float[] newMouseCoords;

    float[] newPositionChange;
    
    float[] newCameraPosition;
    
    float[] zoomCoords;
    
    float zoomFactor;

    void Start()
    {
        cam = Camera.main;

        oldMouseCoords = new float[2];
        newMouseCoords = new float[2];

        newPositionChange = new float[2];
        
        newCameraPosition = new float[2];

        zoomCoords = new float[2];

        newMouseCoords[0] = cam.ScreenToWorldPoint(Input.mousePosition)[0];
        newMouseCoords[1] = cam.ScreenToWorldPoint(Input.mousePosition)[1];

        newMouseCoords.CopyTo(oldMouseCoords, 0);
        
        sensitivity = -1f;
    }
    
    void Update()
    {
        newMouseCoords[0] = cam.ScreenToWorldPoint(Input.mousePosition)[0] - transform.position.x;
        newMouseCoords[1] = cam.ScreenToWorldPoint(Input.mousePosition)[1] - transform.position.y;
        
        newPositionChange[0] = newMouseCoords[0] - oldMouseCoords[0];
        newPositionChange[1] = newMouseCoords[1] - oldMouseCoords[1];
        
        if (Input.GetKey(KeyCode.Mouse2))
        {
            newCameraPosition[0] = Mathf.Clamp(transform.position.x + sensitivity * (newPositionChange[0]), -544.5f, 544.5f);
            newCameraPosition[1] = Mathf.Clamp(transform.position.y + sensitivity * (newPositionChange[1]), -544.5f, 544.5f);

            transform.position = new Vector3 (newCameraPosition[0], newCameraPosition[1], -1);
        }
        
        if ((37.5f < cam.orthographicSize && Input.mouseScrollDelta.y > 0) || (600 > cam.orthographicSize && Input.mouseScrollDelta.y < 0))
        {
            zoomFactor = Mathf.Pow(2, Input.mouseScrollDelta.y);

            zoomCoords[0] = Mathf.Clamp(transform.position.x + ((zoomFactor - 1) * newMouseCoords[0]) / zoomFactor, -544.5f, 544.5f);
            zoomCoords[1] = Mathf.Clamp(transform.position.y + ((zoomFactor - 1) * newMouseCoords[1]) / zoomFactor, -544.5f, 544.5f);

            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize / zoomFactor, 37.5f, 600);
            transform.position = new Vector3(zoomCoords[0], zoomCoords[1], -1);

            newMouseCoords[0] = cam.ScreenToWorldPoint(Input.mousePosition)[0] - transform.position.x;
            newMouseCoords[1] = cam.ScreenToWorldPoint(Input.mousePosition)[1] - transform.position.y;
        }
        
        newMouseCoords.CopyTo(oldMouseCoords, 0);
    }
}
