using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class QuestionModule : UIComposition
{
    [SerializeField] private UIWindow _window;
    [SerializeField] private UIText _question;
    [SerializeField] private List<UIButton> _answers;

    public override void OnClose()
    {
        
    }
    public override void OnLoad()
    {
        _answers[0]?.EnableControls(InputActions);
        _question.PrintText
            (
                "This is a test, dawg. Now what do you say we doggo around the block like a million times cuz what else is there to do?," +
                " Except literally everything. I'm not sure what else to lorem ipsum lorem ipsumlorem ipsum lorem ipsum lorem ipsum lorem ipsum!"
            );
    }
    //public void EnableControls(InputActions inputActions)
    //{
    //    _inputActions = inputActions;
    //}
    //public void DisableControls(InputActions inputActions)
    //{
    //    _inputActions = null;
    //}
}
