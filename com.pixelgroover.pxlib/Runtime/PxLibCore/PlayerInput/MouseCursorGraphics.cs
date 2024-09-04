using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorGraphics 
{
    private Anim _mouseCursorIdle;
    private Anim _mouseCursorClick;
    private Anim _mouseCursorHover;
    private Anim _mouseCursorGrab;
    
    public MouseCursorGraphics(Anim idle, Anim click = null, Anim hover = null, Anim grab = null)
    {
        _mouseCursorIdle = idle;
        _mouseCursorClick = click != null ? click : null;
        _mouseCursorHover = hover != null ? hover : null;
        _mouseCursorGrab = grab != null ? grab: null;
        Init();
    }
    private void Init()
    {
        //Sprite sprite = _mouseCursorIdle.Frames[0];
        //sprite.ToTexture
        Debug.Log("init mouse graphics");
        //Cursor.SetCursor(_mouseCursorIdle.Frames[0].texture, Vector3.zero, CursorMode.ForceSoftware);
        Cursor.SetCursor(_mouseCursorIdle.Frames[0].ConvertSpriteToTexture(), Vector3.zero, CursorMode.ForceSoftware);
    }
    private void OnCursorIdle()
    {

    }
    private void OnCursorClick()
    {

    }
    private void OnCursorHover()
    {

    }
    private void OnCursorGrab()
    {

    }
}
