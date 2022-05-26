using UnityEngine;

public class CursorControl : MonoBehaviour
{
    [SerializeField] private Texture2D cursorNormalTexture;
    [SerializeField] private Texture2D cursorAttackTexture;
    [SerializeField] private Texture2D cursorCross;
    private static Texture2D staticCursorNormalTexture;
    private static Texture2D staticCursorAttackTexture;
    private static Texture2D staticCursorCross;
    private static Texture2D staticCursorBuilding;
    private static bool isBuilding = false;
    private static bool isObject = false;

    public static bool IsBuilding
    { 
        get => isBuilding;
        set => isBuilding = value;
    }
    
    public static bool IsObject()
        => isObject;

    private void Start()
    {
        staticCursorNormalTexture = cursorNormalTexture;
        staticCursorAttackTexture = cursorAttackTexture;
        staticCursorCross = cursorCross;
    }

    public static void SetAttackCursor()
    {
        if(!isBuilding)
            Cursor.SetCursor(staticCursorAttackTexture, Vector2.zero, CursorMode.Auto);
        else
        {
            Cursor.SetCursor(staticCursorCross, Vector2.zero, CursorMode.Auto);
            isObject = true;
        }
    }

    public static void OutOfRadius()
    {
        Cursor.SetCursor(staticCursorCross, Vector2.zero, CursorMode.Auto);
    }

    public static void SetBuildingCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        staticCursorBuilding = texture;
    }

    public static void SetNormalCursor()
    {
        if(!isBuilding)
            Cursor.SetCursor(staticCursorNormalTexture, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(staticCursorBuilding, Vector2.zero, CursorMode.Auto);
        isObject = false;
    }
}