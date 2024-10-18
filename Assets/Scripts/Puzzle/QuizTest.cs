using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizTest : MonoBehaviour
{
    public Quiz1 quiz1;
    public Quiz1 quiz2;
    void Start()
    {
        quiz1.OnQuizClear += QuizClearAction;
        quiz2.OnQuizClear += QuizClearAction;
    }

    // Update is called once per frame
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

    }

    public void QuizClearAction()
    {
        Debug.Log("Quiz Clear Action in Quiz Test");
    }

    private void OnDestroy()
    {
        quiz1.OnQuizClear -= QuizClearAction;
        quiz2.OnQuizClear -= QuizClearAction;
    }

}
