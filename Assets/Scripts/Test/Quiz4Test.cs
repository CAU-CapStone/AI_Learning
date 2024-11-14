using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Quiz4Test : Interaction
{
    private IQuiz quiz;

    public override void Activate()
    {
        quiz.startQuiz();
        quiz.OnQuizClear += OnSuccess;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        quiz = QuizDictionary.Instance.GetQuiz("Quiz4");
    }

    void OnSuccess()
    {
        Debug.Log("Quiz4 success");
    }

    void OnFail()
    {
        Debug.Log("Quiz4 failed");
    }
}
