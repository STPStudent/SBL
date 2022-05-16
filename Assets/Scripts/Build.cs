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
                var coordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                IsBuilding = false;
                Instantiate(Fabric, new Vector3(coordinates.x, coordinates.y, 0), Quaternion.identity);
                Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
            }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (resources.resourcesCount >= Cost)
        {
            IsBuilding = true;
            resources.resourcesCount -= Cost;
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }
}