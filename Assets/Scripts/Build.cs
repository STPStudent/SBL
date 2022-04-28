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
    public int Cost;
    [SerializeField] private ResourcesFabric resources;
    public Texture2D cursor;
    [SerializeField] private Texture2D normalCursor;

    public void Update()
    {
        if (IsBuilding)
            if (Input.GetMouseButtonDown(1))
            {
                IsBuilding = false;
                var coordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(Fabric, new Vector3(coordinates.x, coordinates.y, 0), Quaternion.identity);
                resources.resourcesCount -= Cost;
                Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
            }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (resources.resourcesCount >= Cost)
        {
            IsBuilding = true;
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }
}