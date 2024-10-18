using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz1 : MonoBehaviour
{
    public QuizBasket smallQuizBasket;
    public QuizBasket largeQuizBasket;

    public event Action OnQuizClear;

    void Update()
    {
        if(smallQuizBasket.isFull() && largeQuizBasket.isFull())
        {
            Debug.Log("Quiz1 Clear");
            gameObject.SetActive(false);
            OnQuizClear?.Invoke();
        }
    }


    public void startQuiz()
    {
        gameObject.SetActive(true);
    }
}
