using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Chef/Anim/BattlerDefaultAnimSet")]
[System.Serializable]
public class BattleActorDefaultAnimSet : ScriptableObject
{
    [SerializeField] Sprite[] _sprites;
    [SerializeField] public Anim idle;
    [SerializeField] public Anim damaged;
    [SerializeField] public Anim dead;
    [SerializeField] public Anim dodgeVertical;
    [SerializeField] public Anim dodgeLeft;
    [SerializeField] public Anim dodgeRight;
    [SerializeField] public Anim dazed;
}
