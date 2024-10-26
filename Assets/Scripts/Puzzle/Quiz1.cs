using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz1 : MonoBehaviour
{
    public QuizBasket smallQuizBasket;
    public QuizBasket largeQuizBasket;

    public event Action OnQuizClear;

    public Camera playerCamera;
    public Camera puzzleCamera;

    void Update()
    {
        if(smallQuizBasket.isFull() && largeQuizBasket.isFull())
        {
            Debug.Log("Quiz Clear");
            gameObject.SetActive(false);
            OnQuizClear?.Invoke();
        }
    }


    public void startQuiz()
    {
        playerCamera.enabled = false;
        puzzleCamera.enabled = true;

        gameObject.SetActive(true);
    }
}
