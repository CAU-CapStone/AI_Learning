using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz4 : MonoBehaviour, IQuiz
{
    public event Action OnQuizClear;

    public void startQuiz()
    {
        
        GameManager.Instance.QuestTextSetActive(false);
        CameraManager.Instance.mainCamera.enabled = false;
        CameraManager.Instance.puzzleCamera.enabled = true;

        gameObject.SetActive(true);
    }


    public void endQuiz()
    {
        SoundManager.Instance.PlaySoundOneShot("SuccessSound", 0.4f);
        if (!GameManager.Instance.isClearPuzzle5)
        {
        }
        GameManager.Instance.SetMagicCircleOutPos(new Vector3(69.93f,21.9f, 42.95f));
        gameObject.SetActive(false);
        OnQuizClear?.Invoke();
    }
}
