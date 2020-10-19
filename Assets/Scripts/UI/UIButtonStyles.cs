using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "ScriptableObjects/UI/ButtonStyle")]
public class UIButtonStyle :ScriptableObject
{
    [BoxGroup("Button States")] public Sprite up;
    [BoxGroup("Button States")] public Sprite hover;
    [BoxGroup("Button States")] public Sprite down;
    [BoxGroup("Button States")] public Sprite disabled;

}
