using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizTest : MonoBehaviour
{
    public IQuiz quiz1;
    public IQuiz quiz2;
    public IQuiz quiz3;

    void Start()
    {
        quiz1 = QuizDictionary.Instance.GetQuiz("Quiz1");
        quiz2 = QuizDictionary.Instance.GetQuiz("Quiz2");
        quiz3 = QuizDictionary.Instance.GetQuiz("Quiz3");

        quiz1.OnQuizClear += QuizClearAction;
        quiz2.OnQuizClear += QuizClearAction;
        quiz3.OnQuizClear += QuizClearAction;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            quiz1.startQuiz();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            quiz2.startQuiz();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            quiz3.startQuiz();
        }
    }

    public void QuizClearAction()
    {
        Debug.Log("Quiz Clear Action in Quiz Test");
    }

    private void OnDestroy()
    {
        quiz1.OnQuizClear -= QuizClearAction;
        quiz2.OnQuizClear -= QuizClearAction;
        quiz3.OnQuizClear -= QuizClearAction;
    }

}
