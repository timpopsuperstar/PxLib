using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Anim/Anim")]
[System.Serializable]

public class Anim : ScriptableObject
{
    public float Duration
    {
        get => 1f / _frameRate * _frames.Count;
        set => _frameRate = value / _frames.Count;
    }
    public bool Loop
    {
        get => _loop;
        set => _loop = value;
    }

    public float FrameRate
    {
        get => _frameRate;
        set => _frameRate = value;
    }

    
    public int FrameCount => _frames.Count;
    public List<Sprite> Frames 
    {
        get => _frames;
        set => _frames = value;
    }

    [SerializeField] private float _frameRate = 6;
    [SerializeField] private bool _loop = false;
    [SerializeField] private List<Sprite> _frames;

    [SerializeField] public AudioClip _sfx;


}
