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
    public QuestionModule QuestionModule { get; private set; }
    public Vector4 answerValues;

    public void Load(InputActions inputActions, QuestionModule questionModule)
    {
        EnableControls(inputActions);
        QuestionModule = questionModule;
    }
    protected override void OnClick()
    {
        QuestionModule.OnAnswer(answerValues);
        DisableControls();
    }

    private void Start()
    {

    }
}
