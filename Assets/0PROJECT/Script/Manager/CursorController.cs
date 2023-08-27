using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private CursorSO CursorData;
    [SerializeField] private LayerMask DetectionLayers;

    private void FixedUpdate()
    {
        SetCursor();
    }

    private ScriptType CheckWhichScriptTargetHas(Component[] components)
    {
        foreach (var component in components)
        {
            if (component is Building)
                return ScriptType.Building;

            if (component is Soldier)
                return ScriptType.Soldier;
        }

        return ScriptType.None;
    }

    private void SetCursor()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f, DetectionLayers);

        if (hit.collider != null)
        {
            ScriptType targetType = CheckWhichScriptTargetHas(hit.collider.GetComponents<Component>());

            Texture2D cursorTexture;

            switch (targetType)
            {
                case ScriptType.Building:
                    cursorTexture = CursorData.Cursor_OnBuilding;
                    break;
                case ScriptType.Soldier:
                    cursorTexture = CursorData.Cursor_OnSoldier;
                    break;
                default:
                    cursorTexture = CursorData.Cursor_Default;
                    break;
            }

            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(CursorData.Cursor_Default, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}