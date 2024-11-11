using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz4 : MonoBehaviour, IQuiz
{
    public event Action OnQuizClear;

    public void startQuiz()
    {
        CameraManager.Instance.mainCamera.enabled = false;
        CameraManager.Instance.puzzleCamera.enabled = true;

        gameObject.SetActive(true);
    }


    public void endQuiz()
    {
        SoundManager.Instance.PlaySoundOneShot("SuccessSound", 0.4f);
        gameObject.SetActive(false);
        OnQuizClear?.Invoke();
    }
}
