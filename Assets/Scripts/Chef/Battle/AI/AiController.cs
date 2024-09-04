using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleActor))]
public class AiController : MonoBehaviour
{
    [SerializeField] private AiRoutine _aiRoutine;
    [SerializeField] private List<BattleAction> _activeActions = new List<BattleAction>();
    private BattleAction _currentAction;    
    private int _routinePosition = 0;
    private BattleActor _actor;
    private bool _active;

    private void Awake()
    {
        _actor = GetComponent<BattleActor>();
    }
    public void RunController()
    {
        _active = true;
        LoadAction(0);
    }
    public void StopController()
    {
        _active = false;
    }
    private void LoadAction(int i)
    {
        _currentAction = _aiRoutine.GetAction(i);
        _activeActions.Add(_currentAction);
        _currentAction.OnActionPerformed += OnActionPerformed;
        _currentAction.Act(_actor);
    }
    private void ResetRoutine()
    {
        _routinePosition = 0;
    }
    private void OnActionPerformed(BattleAction action)
    {
        if (!_active)
        {
            return;
        }
        StartCoroutine(IEOnActionPerformed(action));
    }
    private IEnumerator IEOnActionPerformed(BattleAction action)
    {
        _activeActions.Remove(action);
        _routinePosition += 1;
        if (_routinePosition >= _aiRoutine.Length)
        {
            ResetRoutine();
        }
        yield return null;
        LoadAction(_routinePosition);
        action.OnActionPerformed -= OnActionPerformed;        
    }
}