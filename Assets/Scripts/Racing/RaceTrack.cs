using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : BezierSpline
{
    public const float startPosDistanceOffset = 16f;

    [SerializeField] private Racer[] _racers;
    public float GetStartPosition(int place)
    {
        return GetPreviousPositionByDistance(startPosDistanceOffset * place, 0f, true);
    }

    private void Start()
    {
        for(int i = 0; i <= _racers.Length -1; i++)
        {
            _racers[i].StartRace(i + 1);
        }
    }
}
