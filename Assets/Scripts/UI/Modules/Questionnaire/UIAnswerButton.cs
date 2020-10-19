using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Runtime.Remoting;

[ExecuteInEditMode]
public class UIAnswerButton : UIButton
{
    public override void OnActivate()
    {
        Debug.Log("Answered!");
    }
}
