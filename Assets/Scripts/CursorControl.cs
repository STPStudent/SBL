using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    [SerializeField] public Texture2D _cursorNormalTexture;
    [SerializeField] public Texture2D _cursorAtackTexture;
    [SerializeField] public  CursorMode _cursorMode = CursorMode.Auto;
    [SerializeField] public Vector2 _hotSpot = Vector2.zero;
    public static Texture2D cursorNormalTexture;
    public static Texture2D cursorAtackTexture;
    public static  CursorMode cursorMode;
    public static Vector2 hotSpot;

    void Start()
    {
        cursorNormalTexture = _cursorNormalTexture;
        cursorAtackTexture = _cursorAtackTexture;
        cursorMode = _cursorMode;
        hotSpot = _hotSpot;
    }
    public static void SetAttakCursor()
    {
        Cursor.SetCursor(cursorAtackTexture, hotSpot, cursorMode);
    }

    public static void SetNormalCursor()
    {
        Cursor.SetCursor(cursorNormalTexture, hotSpot, cursorMode);
    }
}
