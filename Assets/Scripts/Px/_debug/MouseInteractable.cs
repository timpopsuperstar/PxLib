using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class MouseInteractable : MonoBehaviour
{
    public BoxCollider2D BoxCollider => _boxCollider ? _boxCollider : (_boxCollider = GetComponent<BoxCollider2D>());
    private BoxCollider2D _boxCollider;
    public SpriteRenderer SpriteRenderer => _spriteRenderer ? _spriteRenderer : (_spriteRenderer = GetComponent<SpriteRenderer>());
    private SpriteRenderer _spriteRenderer;


    public abstract void OnLeftClick(Vector2 mousePos);
    public abstract void OnLeftRelease(Vector2 mousePos);
    public abstract void OnMouseMove(Vector2 mousePos);
}
