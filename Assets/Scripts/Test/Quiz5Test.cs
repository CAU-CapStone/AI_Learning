using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz5Test : Interaction
{
    private Quiz5 quiz;
    
    public override void Activate()
    {
        quiz.startQuiz();
        quiz.OnQuizClear += OnSuccess;
        quiz.OnQuizFail += OnFail;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        quiz = QuizDictionary.Instance.GetQuiz("Quiz5") as Quiz5;
    }

    void OnSuccess()
    {
        Debug.Log("Quiz5 success");
    }

    void OnFail()
    {
        Debug.Log("Quiz5 failed");
    }
}
