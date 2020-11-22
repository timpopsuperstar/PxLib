using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(SpriteRenderer))]

public class SimpleAnimator : MonoBehaviour
{
    public bool Done => _currentFrame >= _currentAnim.FrameCount;
    public Anim CurrentAnim => _currentAnim;
    public float FrameRate => 1f / _currentAnim.FrameRate;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public int CurrentFrame
    {
        get => _currentFrame;
        set
        {
            _currentFrame = Mathf.Clamp(value, 0, _currentAnim.FrameCount-1);
            _spriteRenderer.sprite = _currentAnim.Frames[_currentFrame];
            _playing = false;
        }
    }
      
    [SerializeField] private Anim _currentAnim;
    private Anim _previous;
    private int _currentFrame;
    private float _nextFrameTime;
    private float _pausedNextFrameTimeDelta;
    private SpriteRenderer _spriteRenderer;
    private bool _playing;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (CurrentAnim)
        {
            Play();
        }
    }
    void Update()
    {
        if (!_playing || Time.time < _nextFrameTime || _spriteRenderer == null)
            return;
        _currentFrame++;

        if (_currentFrame >= _currentAnim.FrameCount)
        {
            if (!CurrentAnim.Loop)
            {
                _playing = false;
                return;
            }
            _currentFrame = 0;
        }
        _spriteRenderer.sprite = _currentAnim.Frames[_currentFrame];
        _nextFrameTime += FrameRate;
    }
    public void PlayAnim(Anim anim)
    {
        if (anim == CurrentAnim || anim == null)
        {
            return;
        }
        _previous = _currentAnim;
        _currentAnim = anim;
        _currentFrame = -1;
        _playing = true;
        _nextFrameTime = Time.time;
    }
    public void LoadAnim(Anim anim)
    {
        if (anim == CurrentAnim || anim == null)
        {
            return;
        }
        _previous = _currentAnim;
        _currentAnim = anim;
        _currentFrame = 0;        
        _pausedNextFrameTimeDelta = 0f;
        _playing = false;
        _spriteRenderer.sprite = _currentAnim.Frames[_currentFrame];
    }
    public void Play()
    {
        _playing = true;
        _nextFrameTime = Time.time + FrameRate - _pausedNextFrameTimeDelta;
    }
    public void Stop()
    {
        _playing = false;
        _currentFrame = -1;
        _pausedNextFrameTimeDelta = 0f;
    }
    public void Pause()
    {
        _playing = false;
        _pausedNextFrameTimeDelta = _nextFrameTime - Time.time;
    }
    private void PlaySfx()
    {
        //    SoundManager.instance.PlaySound(current.sfx);
    }
}
