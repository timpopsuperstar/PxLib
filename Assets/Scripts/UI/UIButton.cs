using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class UIButton : MonoBehaviour
{    
    public enum State { up,hover, down, disabled};    

    public Sprite up;
    public Sprite hover;
    public Sprite down;
    public Sprite disabled;

    private State state;
    [SerializeField] private State previousState;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collision;
    private bool isActive;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collision = GetComponent<BoxCollider2D>();
        SetState(State.up);
        EnableControls(Scratchpad.instance.inputActions);
    }

    public void SetState(State state)
    {
        if (state == this.state) return;

        previousState = this.state;
        this.state = state;
        switch (state)
        {
            case State.up:
                spriteRenderer.sprite = up;
                break;
            case State.hover:
                spriteRenderer.sprite = hover;
                break;
            case State.down:
                spriteRenderer.sprite = down;
                break;
            case State.disabled:
                spriteRenderer.sprite = disabled;
                break;
        }        
    }

    void OnPointerOver()
    {
        if(state != State.disabled && !isActive)
        {
            SetState(State.hover);
        }
    }

    void OnPointerExit()
    {
        SetState(previousState);
    }

    private void OnPointerRightClick()
    {
        if (!isActive)
        {
            StartCoroutine(Activate());
        }
    }

    IEnumerator Activate()
    {
        if (isActive) yield break;

        isActive = true;
        var pos = transform.position;
        pos.y -= 1;
        transform.position = pos;
        SetState(State.down);
        yield return new WaitForSeconds(.10f);
        pos.y += 1;
        transform.position = pos;
        SetState(State.up);
        yield return new WaitForSeconds(.06f);
        isActive = false;

        SceneManager.LoadScene(2);
    }


    public void EnableControls(InputActions inputActions)
    {
        inputActions.OnPointerPosition += OnPointerPosition;
        inputActions.OnMouseLeftClick += OnClick;
    }
    public void DisableControls(InputActions inputActions)
    {
        inputActions.OnPointerPosition -= OnPointerPosition;
        inputActions.OnMouseLeftClick -= OnClick;
    }

    public void OnPointerPosition(Vector2 v)
    {
        var xMin = collision.bounds.center.x - collision.bounds.extents.x;
        var xMax = collision.bounds.center.x + collision.bounds.extents.x;
        var yMin = collision.bounds.center.x - collision.bounds.extents.y;
        var yMax = collision.bounds.center.x + collision.bounds.extents.y;

        if(v.x > xMin && v.x < xMax)
        {
            if(v.y > yMin && v.y < yMax)
            {
                OnPointerOver();
                return;
            }
        }
        if (!isActive && state != State.up && state != State.disabled)
        {
            OnPointerExit();
        }
    }

    public void OnClick(Vector2 v)
    {
        if (PointerIsOver(v)) StartCoroutine(Activate());        
    }    

    public bool PointerIsOver(Vector2 v)
    {
        var xMin = collision.bounds.center.x - collision.bounds.extents.x;
        var xMax = collision.bounds.center.x + collision.bounds.extents.x;
        var yMin = collision.bounds.center.x - collision.bounds.extents.y;
        var yMax = collision.bounds.center.x + collision.bounds.extents.y;

        if (v.x > xMin && v.x < xMax)
        {
            if (v.y > yMin && v.y < yMax)
            {
                return true;
            }
        }
        return false;
    }
}
