using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Anim/DirectionalAnimSet")]
public class DirectionalAnimSet : ScriptableObject
{
    [SerializeField] private List<Anim> _anims;

    public List<Anim> Anims
    {
        get => _anims;
        set
        {
            if(_anims == null)
            {
                _anims = value;
                return;
            }
            Debug.LogWarning("Cannot overwrite DirectionalAnimSet");
            return;
        }
    }

    public Anim GetAnim(Direction d)
    {
        switch (_anims.Count)
        {
            case 1:
                return _anims[0];                
            case 4:
                return _anims[(int)d / 2];
            case 8:
                return _anims[(int)d];
        }
        if (_anims.Count > 0)
            return _anims[0];
        return null;
    }
}
