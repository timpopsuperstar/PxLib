using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class Racer : MonoBehaviour
{
    public int Place
    {
        get => _place;
        set => _place = value;
    }
    public float Progress
    {
        get => _progress;
        set
        {
            _previousProgress = _progress;
            _progress = Mathf.Clamp01(value);
        }
    }
    public enum RacerMode
    {
        Once,
        Loop,
        PingPong
    }
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }
    public int CompletedLaps
    {
        get => _laps;
    }
    public RaceTrack Track
    {
        get => _raceTrack;
        set => _raceTrack = value;
    }
    [SerializeField] private float _speed;
    [SerializeField] private RaceTrack _raceTrack;
    [SerializeField] [Range(1,8)] private int _place;
    [SerializeField] private int _laps = -1;
    
    private bool _goingForward = true;
    private float _progress;
    private float _previousProgress;


    public void StartRace(int place)
    {
        _progress = _raceTrack.GetStartPosition(place);
        StartCoroutine(IEMove());
    }

    private void Start()
    {
        
        
    }
    private void Update()
    {
    }

    private IEnumerator IEMove()
    {
        while (true)
        {
            Progress = _raceTrack.GetNextPositionByDistance(_speed, _progress, true);
            if(_progress < _previousProgress)
            {
                _laps++;
            }
            Vector3 position = _raceTrack.GetPoint(_progress);
            transform.localPosition = position;
            yield return new WaitForSeconds(1/60f);
        }
    }
}


//Loop Logic
//if (_goingForward)
//{
//    _progress += Time.deltaTime / duration;
//    if (_progress > 1f)
//    {
//        switch (_mode)
//        {
//            case RacerMode.Once:
//                _progress = 1f;
//                break;
//            case RacerMode.Loop:
//                _progress -= 1f;
//                break;
//            case RacerMode.PingPong:
//                _progress = 2f - _progress;
//                _goingForward = false;
//                break;
//        }
//    }
//}
//else
//{
//    _progress -= Time.deltaTime / duration;
//    if (_progress < 0f)
//    {
//        _progress = -_progress;
//        _goingForward = true;
//    }
//}
//_progress = _raceTrack.GetNextPositionByDistance(_speed, _progress, true);

//Vector3 position = _raceTrack.GetPoint(_progress);
//transform.localPosition = position;
