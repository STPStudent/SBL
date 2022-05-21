using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Build : MonoBehaviour, IPointerDownHandler
{
    public GameObject Fabric;
    [SerializeField] private Image circle;
    public int Cost;
    [SerializeField] private MainBuilding mainBuilding;
    public Texture2D cursor;
    [SerializeField] private Texture2D normalCursor;
    [SerializeField] private CursorControl cursorControl;
    private bool isBuilding = false;

    public void Update()
    {
        if (circle.fillAmount - Time.deltaTime / 15 < 0)
            circle.fillAmount = 0;
        else
            circle.fillAmount -= Time.deltaTime / 15;
        
        if (isBuilding)
        {
            var t = true;
            var coordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var allBildings = GameObject
                .FindGameObjectsWithTag(Fabric.tag);
                foreach(var obj in allBildings)
                {
                    Debug.Log(obj.name);
                    if((obj.name.Contains("PlayerMainBuild") 
                        && (obj.transform.position - coordinates).magnitude < 30
                    ||(obj.transform.position - coordinates).magnitude < 20)
                    && !obj.gameObject.name.Contains("Unit"))
                    {
                        t = false;
                        break;
                    }
                }
            if(t || cursorControl.IsObjekt())
                CursorControl.OutOfRadius();
            else
                CursorControl.SetNormalCursor();
            if (Input.GetMouseButtonDown(1)
            && !cursorControl.IsObjekt()
            && !t)
            {
                isBuilding = false;
                cursorControl.IsBuilding = false;
                Instantiate(Fabric, new Vector3(coordinates.x, coordinates.y, 0), Quaternion.identity);
                mainBuilding.resourcesCount -= Cost;
                CursorControl.SetNormalCursor();
                circle.fillAmount = 1f;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (mainBuilding.resourcesCount >= Cost)
        {
            isBuilding = true;
            cursorControl.IsBuilding = true;
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            cursorControl.cursorBuilding = cursor;
        }
    }
}