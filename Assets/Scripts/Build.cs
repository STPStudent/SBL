using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Build : MonoBehaviour
{
    internal bool isBuilding;
    internal GameObject Fabric{ get; set; }
    internal int Cost;
    internal BuildPanel panel;
    [SerializeField] private MainBuilding mainBuilding;
    [SerializeField] private Text text;

    public void Update()
    {
        if (isBuilding)
        {
            var coordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var t = true;
            var allBildings = GameObject
                .FindGameObjectsWithTag(Fabric.tag);
            foreach (var obj in allBildings)
            {
                if ((obj.name.Contains("PlayerMainBuild")
                     && (obj.transform.position - coordinates).magnitude < 30
                     || (obj.transform.position - coordinates).magnitude < 20)
                    && !obj.gameObject.name.Contains("Unit"))
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
                panel.circle.fillAmount = 1f;
                if(Fabric.gameObject.name.Contains("Recourse"))
                {
                    Cost *= 2;
                    text.text = Cost.ToString();
                }
            }
            else
                CursorControl.SetNormalCursor();
        }
    }
}