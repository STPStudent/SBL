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
    [SerializeField] private MainBuilding mainBuilding;
    [SerializeField] private Texture2D cursor;

    public void Update()
    {
        if (isBuilding)
        {
            var coordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var t = true;
            var allBuildings = GameObject
                .FindGameObjectsWithTag(Fabric.tag);
            foreach (var build in allBuildings)
            {
                Debug.Log(build.name);
                if ((build.name.Contains("PlayerMainBuild")
                     && (build.transform.position - coordinates).magnitude < 30
                     || (build.transform.position - coordinates).magnitude < 20)
                    && !build.gameObject.name.Contains("Unit"))
                {
                    t = false;
                    break;
                }
            }

            if (t || CursorControl.IsObject())
                CursorControl.OutOfRadius();
            else if (Input.GetMouseButtonDown(1))
            {
                isBuilding = false;
                CursorControl.IsBuilding = false;
                Instantiate(Fabric, new Vector3(coordinates.x, coordinates.y, 0), Quaternion.identity);
                mainBuilding.resourcesCount -= Cost;
                CursorControl.SetNormalCursor();
                circle.fillAmount = 1f;
            }
            else
                CursorControl.SetNormalCursor();
        }

        if (circle.fillAmount - Time.deltaTime / 15 < 0)
            circle.fillAmount = 0;
        else
            circle.fillAmount -= Time.deltaTime / 15;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (mainBuilding.resourcesCount >= Cost && circle.fillAmount == 0)
        {
            isBuilding = true;
            CursorControl.IsBuilding = true;
            CursorControl.SetBuildingCursor(cursor);
        }
    }
}