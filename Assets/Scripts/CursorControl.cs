using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    [SerializeField] public Texture2D _cursorTexture;
    [SerializeField] public  CursorMode _cursorMode = CursorMode.Auto;
    [SerializeField] public Vector2 _hotSpot = Vector2.zero;
    public static Texture2D cursorTexture;
    public static  CursorMode cursorMode;
    public static Vector2 hotSpot;

    void Start()
    {
        cursorTexture = _cursorTexture;
        cursorMode = _cursorMode;
        hotSpot = _hotSpot;
    }
    public static void MouseAttak()
    {
        var k = Texture2D.redTexture;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
