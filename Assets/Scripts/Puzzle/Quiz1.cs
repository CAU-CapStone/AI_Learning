using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz1 : MonoBehaviour, IQuiz
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
            
            //퀴즈 클리어시 사운드 효과
            SoundManager.Instance.PlaySoundOneShot("SuccessSound", 0.4f);
        }
    }


    public void startQuiz()
    {
        GameManager.Instance.QuestTextSetActive(false);
        playerCamera.enabled = false;
        puzzleCamera.enabled = true;

        gameObject.SetActive(true);
    }
}
