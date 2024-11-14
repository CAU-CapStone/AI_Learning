using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quiz5Color
{
    None,
    Red,
    Green,
    Blue
}

public class Quiz5 : MonoBehaviour, IQuiz
{
    public event Action OnQuizClear;
    public event Action OnQuizFail;

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
        //퀴즈 클리어시 사운드 효과
        SoundManager.Instance.PlaySoundOneShot("SuccessSound", 0.4f);
    }

    public void failQuiz()
    {
        OnQuizFail?.Invoke();
    }
}
