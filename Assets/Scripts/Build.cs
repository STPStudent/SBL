using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class Build : MonoBehaviour, IPointerDownHandler
{
    private bool IsBuilding;
    public GameObject Fabric;
    public GameObject mainCam;

    public void LateUpdate()
    {
        if (IsBuilding)
            if (Input.GetMouseButtonDown(1))
            {
                IsBuilding = false;
                Debug.Log("false");
                var coordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(Fabric, coordinates, Quaternion.identity);
            }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsBuilding = true;
        Debug.Log("true");
    }
}