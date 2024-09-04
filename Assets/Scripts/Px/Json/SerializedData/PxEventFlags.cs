using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

[System.Serializable]
public class PxEventFlags //: PxJsonData
{
    private List<string> EventFlagList
    {
        get
        {
            if (_eventFlagList != null)
            {
                return _eventFlagList;
            }
            if(_eventFlagArray != null)
            {
                _eventFlagList = _eventFlagArray.ToList();
                return _eventFlagList;
            }
            _eventFlagList = new List<string>();
            return _eventFlagList;
        }
    }
    private List<string> _eventFlagList;
    [SerializeField] private string[] _eventFlagArray;

    public void Add(string s)
    {
        EventFlagList.Add(s);
        UpdateArray();
    }
    public void Remove(string s)
    {
        EventFlagList.Remove(s);
        UpdateArray();
    }
    public void Clear()
    {
        EventFlagList.Clear();
        UpdateArray();
    }
    public bool Contains(string s) => EventFlagList.Contains(s) == true ? true : false;

    private void UpdateArray()
    {
        var newArray = EventFlagList.ToArray();
        _eventFlagArray = newArray;
    }
}
