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
    [SerializeField] private Texture2D notCursor;
    private Vector3 coordinatesBuildEnemy;
    private bool isNotBuild;

    public void Start()
    {
        coordinatesBuildEnemy = Camera.main.ScreenToWorldPoint(GameObject.Find("EnemyMainBuild").transform.position);
    }

    public void Update()
    {
        if (isBuilding)
        {
            var coordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if ((coordinatesBuildEnemy - coordinates).magnitude > 35)
            {
                isNotBuild = true;
                Cursor.SetCursor(notCursor, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                isNotBuild = false;
                Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            }
            if (Input.GetMouseButtonDown(1) && !isNotBuild)
            {
                isBuilding = false;
                Instantiate(Fabric, new Vector3(coordinates.x, coordinates.y, 0), Quaternion.identity);
                resources.resourcesCount -= Cost;
                Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
                circle.fillAmount = 1f;
            }
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