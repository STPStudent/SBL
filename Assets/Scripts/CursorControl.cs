using UnityEngine;

public class CursorControl : MonoBehaviour
{
    [SerializeField] private Texture2D cursorNormalTexture;
    [SerializeField] private Texture2D cursorAttackTexture;
    private static Texture2D staticCursorNormalTexture;
    private static Texture2D staticCursorAttackTexture;

    private void Start()
    {
        staticCursorNormalTexture = cursorNormalTexture;
        staticCursorAttackTexture = cursorAttackTexture;
    }

    public static void SetAttackCursor()
    {
        Cursor.SetCursor(staticCursorAttackTexture, Vector2.zero, CursorMode.Auto);
    }

    public static void SetNormalCursor()
    {
        Cursor.SetCursor(staticCursorNormalTexture, Vector2.zero, CursorMode.Auto);
    }
}