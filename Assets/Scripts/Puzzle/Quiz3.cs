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
        GameManager.Instance.QuestTextSetActive(false);
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
}
