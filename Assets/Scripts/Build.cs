using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Build : MonoBehaviour, IPointerDownHandler
{
    private bool isBuilding;
    public GameObject Fabric;
    [SerializeField] private Image circle;
    public int Cost;
    [SerializeField] private ResourcesFabric resources;
    public Texture2D cursor;
    [SerializeField] private Texture2D normalCursor;
    

    public void Update()
    {
        if (isBuilding)
            if (Input.GetMouseButtonDown(1))
            {
                var coordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isBuilding = false;
                Instantiate(Fabric, new Vector3(coordinates.x, coordinates.y, 0), Quaternion.identity);
                resources.resourcesCount -= Cost;
                Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
                circle.fillAmount = 1f;
            }

        if (circle.fillAmount - Time.deltaTime / 15 < 0)
            circle.fillAmount = 0;
        else
            circle.fillAmount -= Time.deltaTime / 15;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (resources.resourcesCount >= Cost && circle.fillAmount == 0)
        {
            isBuilding = true;
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }
}