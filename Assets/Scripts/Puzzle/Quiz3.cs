using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz3 : MonoBehaviour, IQuiz
{
    public event Action OnQuizClear;

    public Camera playerCamera;
    public Camera puzzleCamera;

    public void startQuiz()
    {
        playerCamera.enabled = false;
        puzzleCamera.enabled = true;

        gameObject.SetActive(true);
    }

    public void endQuiz()
    {
        gameObject.SetActive(false);
        OnQuizClear?.Invoke();
    }
}
