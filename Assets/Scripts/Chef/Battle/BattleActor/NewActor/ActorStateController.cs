using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStateController : MonoBehaviour
{  
    public ActorState State
    {
        get => _currentState;
        set
        {
            if (_currentState != null)
            {
                _previousStates.Add(_currentState);
            }
            _currentState = value;
            _currentState.PerformAction();
        }
    }

    private ActorState _currentState;
    private List<ActorState> _previousStates = new List<ActorState>();
}
