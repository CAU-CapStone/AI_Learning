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
        
        GameManager.Instance.QuestTextSetActive(false);
        playerCamera.enabled = false;
        puzzleCamera.enabled = true;

        gameObject.SetActive(true);
    }

    public void endQuiz()
    {
        if (!GameManager.Instance.isClearPuzzle4)
        {
            GameManager.Instance.SetQuestText("다른 도음이 필요한 사람을 찾아보자");
        }
        OnQuizClear?.Invoke();
        //퀴즈 클리어시 사운드 효과
        SoundManager.Instance.PlaySoundOneShot("SuccessSound", 0.4f);
        gameObject.SetActive(false);
    }

    public void failQuiz()
    {
        OnQuizFail?.Invoke();
    }
}
