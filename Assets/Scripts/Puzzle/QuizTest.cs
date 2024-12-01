using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizTest : MonoBehaviour
{
    //public IQuiz quiz1;
    //public IQuiz quiz2;
    //public IQuiz quiz3;
    public IQuiz dtQuiz1;
    public IQuiz dtQuiz2;

    void Start()
    {
        //quiz1 = QuizDictionary.Instance.GetQuiz("Quiz1");
        //quiz2 = QuizDictionary.Instance.GetQuiz("Quiz2");
        //quiz3 = QuizDictionary.Instance.GetQuiz("Quiz3");
        dtQuiz1 = QuizDictionary.Instance.GetQuiz("DTQuiz1");
        dtQuiz2 = QuizDictionary.Instance.GetQuiz("DTQuiz2");

        //quiz1.OnQuizClear += QuizClearAction;
        //quiz2.OnQuizClear += QuizClearAction;
        //quiz3.OnQuizClear += QuizClearAction;
        dtQuiz1.OnQuizClear += QuizClearAction;
        dtQuiz2.OnQuizClear += QuizClearAction;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //quiz1.startQuiz();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //quiz2.startQuiz();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //quiz3.startQuiz();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            dtQuiz1.startQuiz();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            dtQuiz2.startQuiz();
        }
    }

    public void QuizClearAction()
    {
        Debug.Log("Quiz Clear Action in Quiz Test");
    }

    private void OnDestroy()
    {
        //quiz1.OnQuizClear -= QuizClearAction;
        //quiz2.OnQuizClear -= QuizClearAction;
        //quiz3.OnQuizClear -= QuizClearAction;
        dtQuiz1.OnQuizClear -= QuizClearAction;
        dtQuiz2.OnQuizClear -= QuizClearAction;
    }

}
