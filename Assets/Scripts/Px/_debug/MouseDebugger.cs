using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MouseDebugger : MonoBehaviour
{
    public BoxCollider2D BoxCollider => _boxCollider ? _boxCollider : (_boxCollider = GetComponent<BoxCollider2D>());
    private BoxCollider2D _boxCollider;
    public SpriteRenderer SpriteRenderer => _spriteRenderer ? _spriteRenderer : (_spriteRenderer = GetComponent<SpriteRenderer>());
    private SpriteRenderer _spriteRenderer;

    private InputActions _input;
    private Vector2 _homePosition;
    public bool Active
    {
        get => _active;
        set 
        {
            _active = value;
            SpriteRenderer.color = value ? Color.yellow : Color.white;
            if (!value)
            {
                _input.OnMousePosition -= OnMouseMove;
                _onClickTransformPos = Vector2.zero;
                return;
            }
            _onClickTransformPos = transform.position;
            _input.OnMousePosition += OnMouseMove;
        }
    }
    private bool _active;
    private Vector2 _onClickTransformPos;
    private Vector2 _onClickMousePos;
    public void Init()
    {
        _homePosition = transform.position;
        _input = GameController.Instance.InputActions;
        _input.OnMouseLeftClick += OnMouseLeftClick;
        _input.OnMouseLeftRelease += OnMouseLeftRelease;
    }
    void Start() { }
    void Update() { }
    private void OnEnable() { }
    private void OnDisable()
    {
        _input.OnMouseLeftClick -= OnMouseLeftClick;
        _input.OnMousePosition -= OnMouseMove;
        _input.OnMouseLeftRelease -= OnMouseLeftRelease;
    }
    private void OnMouseLeftClick(Vector2 mousePos)
    {
        //var hits = Raycaster.CastRay(mousePos);
        //Debug.Log(hits.Count);
        //for(int i = 0; i < hits.Count; i++)
        //{
        //    Debug.Log(hits[i]);
        //    if(hits[i] == this)
        //    {
        //        Debug.Log("got myself");
        //        _onClickMousePos = mousePos;
        //        Active = !Active;
        //    }
        //}
    }
    private void OnMouseMove(Vector2 mousePos)
    {
        Debug.Log(mousePos);
        if (!Active)
            return;
        var mouseDelta = _onClickMousePos - mousePos;
        transform.position = _onClickTransformPos - mouseDelta;
    }
    private void OnMouseLeftRelease(Vector2 mousePos)
    {
        if (!Active) return;
        Active = false;
        StartCoroutine(PxTweens.IEMoveOverTime(transform, transform.position, _homePosition, .1f));
    }    
}
