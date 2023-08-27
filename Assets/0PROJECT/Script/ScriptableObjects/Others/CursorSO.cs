using UnityEngine;

[CreateAssetMenu(fileName = "CursorData", menuName = "Cursor/Cursor Data", order = 1)]
public class CursorSO : ScriptableObject
{
    public Texture2D Cursor_Default;
    public Texture2D Cursor_OnBuilding;
    public Texture2D Cursor_OnSoldier;
}
