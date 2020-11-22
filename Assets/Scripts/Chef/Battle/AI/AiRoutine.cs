using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Chef/ScriptableObjects/AiRoutine")]
public class AiRoutine : ScriptableObject
{
    [SerializeField] BattleAction[] _actions;
    public int Length => _actions.Length;
    public BattleAction GetAction(int index)
    {
        index = Mathf.Clamp(index, 0, _actions.Length - 1);
        return _actions[index];
    }
}
