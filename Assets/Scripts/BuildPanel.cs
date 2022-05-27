using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildPanel : MonoBehaviour, IPointerDownHandler
{
    public GameObject Fabric;
    [SerializeField] public Image circle;
    public int Cost;
    [SerializeField] private MainBuilding mainBuilding;
    [SerializeField] private Texture2D cursor;
    [SerializeField] private Build build;

    public void Update()
    {
        if (circle.fillAmount - Time.deltaTime / 15 < 0)
            circle.fillAmount = 0;
        else
            circle.fillAmount -= Time.deltaTime / 15;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (mainBuilding.resourcesCount >= Cost && circle.fillAmount == 0)
        {
            build.isBuilding = true;
            CursorControl.IsBuilding = true;
            build.Fabric = Fabric;
            build.Cost = Cost;
            build.panel = this;
            CursorControl.SetBuildingCursor(cursor);
        }
    }
}