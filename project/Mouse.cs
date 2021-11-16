using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Texture2D cursorArrow;
    public Texture2D cursorHover;

    void Start()
    {
       Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware); 
    }

    // these two are not working correctly
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorHover, Vector2.zero, CursorMode.ForceSoftware); 
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware); 
    }
    
}
