using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using NaughtyAttributes;

public class QuestionModule : UIModule
{
    //[SerializeField] [ResizableTextArea] private string _questionText;
    [SerializeField] private UIWindow _window;
    [SerializeField] private UIText _question;
    [SerializeField] private List<UIAnswerButton> _answers;
    //[SerializeField] [ReadOnly] private QuestionnaireScene _questionnaireScene;

    public override void Close()
    {
        foreach(UIButton b in _answers)
        {
            b.DisableControls();
            Destroy(b.gameObject);
        }
        Destroy(this.gameObject);
    }

    protected override void OnLoad()
    {
        if (_answers.Any())
        {
            foreach (UIAnswerButton answerButton in _answers)
            {
                answerButton.Load(ParentScene.InputActions, this);
            }
        }
        _question.PrintText(_question.Text.text);
    }
    protected override void OnClose()
    {
        Destroy(this.gameObject);
    }
    public void OnAnswer(Vector3 answerVals)
    {
        QuestionnaireScene q = (QuestionnaireScene)ParentScene;
        q.RecordAnswer(answerVals);
    }
}
