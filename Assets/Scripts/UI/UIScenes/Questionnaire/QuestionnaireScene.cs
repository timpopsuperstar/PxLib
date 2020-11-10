using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class QuestionnaireScene : UIScene
{
    public List<QuestionModule> _questions;

    private QuestionModule _activeQuestion;
    [SerializeField] [ReadOnly] private Vector3 _answerValues;
    [SerializeField] [ReadOnly] private bool _waitingForAnswer;

    public override void OnLoad()
    {
        Debug.Log("on load");
        RunQuestion();
    }
    public override void OnClose()
    {
        
    }

    public void RecordAnswer(Vector3 answerVals)
    {
        Debug.Log("Got answer");
        _answerValues += answerVals;
        _activeQuestion.Close();
        _waitingForAnswer = false;
    }

    private void RunQuestion()
    {
        StartCoroutine(IERunQuestions());
    }

    private IEnumerator IERunQuestions()
    {
        for(int i = 0; i < _questions.Count; i++)
        {
            LoadQuestion(i);
            while (_waitingForAnswer)
            {
                yield return new WaitForEndOfFrame();
            }
        }               
        Debug.Log("Questions Over");
    }
    private void LoadQuestion(int i)
    {
        _activeQuestion = Instantiate(_questions[i]);
        _activeQuestion.Load(this);
        _waitingForAnswer = true;
    }
}
