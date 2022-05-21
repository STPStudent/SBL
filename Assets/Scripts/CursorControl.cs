using UnityEngine;

public class CursorControl : MonoBehaviour
{
    [SerializeField] private Texture2D cursorNormalTexture;
    [SerializeField] private Texture2D cursorAttackTexture;
    [SerializeField] private Texture2D cursorCross;
    private static Texture2D staticCursorNormalTexture;
    private static Texture2D staticCursorAttackTexture;
    private static Texture2D staticCursorCross;
    public Texture2D cursorBuilding;
    private static Texture2D staticCursorBuilding;
    private static bool isBuilding = false;
    private static bool isObjekt = false;

    public bool IsBuilding
    { 
        get => isBuilding;
        set => isBuilding = value;
    }
    
    public bool IsObjekt()
        => isObjekt;

    private void Start()
    {
        staticCursorNormalTexture = cursorNormalTexture;
        staticCursorAttackTexture = cursorAttackTexture;
        staticCursorCross = cursorCross;
    }

    void Update()
    {
        staticCursorBuilding = cursorBuilding;
    }

    public static void SetAttackCursor()
    {
        if(!isBuilding)
            Cursor.SetCursor(staticCursorAttackTexture, Vector2.zero, CursorMode.Auto);
        else
        {
            Cursor.SetCursor(staticCursorCross, Vector2.zero, CursorMode.Auto);
            isObjekt = true;
        }
    }

    public static void OutOfRadius()
    {
        Cursor.SetCursor(staticCursorCross, Vector2.zero, CursorMode.Auto);
    }

    public static void SetNormalCursor()
    {
        if(!isBuilding)
            Cursor.SetCursor(staticCursorNormalTexture, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(staticCursorBuilding, Vector2.zero, CursorMode.Auto);
        isObjekt = false;
    }
}