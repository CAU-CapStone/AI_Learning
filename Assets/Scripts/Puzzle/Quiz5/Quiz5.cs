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

    public event Action OnPlayerStart;
    public event Action OnQuizRetry;

    public bool isStart = false;
    
    public Camera playerCamera;
    public Camera puzzleCamera;

    private Vector3 beforeCameraPos;
    private Quaternion beforeCameraRot;

    public GameObject failCanvas;

    public void startQuiz()
    {
        GameManager.Instance.QuestTextSetActive(false);
        playerCamera.enabled = false;
        puzzleCamera.enabled = true;
        
        beforeCameraPos = puzzleCamera.transform.position;
        beforeCameraRot = puzzleCamera.transform.rotation;
        
        puzzleCamera.transform.position = new Vector3(-1.67999995f, 2.31999993f, -7.05999994f + 450);
        puzzleCamera.transform.rotation = new Quaternion(0.186287686f, 0, 0, 0.982495248f);
        
        gameObject.SetActive(true);
    }

    public void startPlayer()
    {
        SoundManager.Instance.PlaySoundOneShot("DialogueButtonSound");
        isStart = true;
        OnPlayerStart?.Invoke();
    }

    public void endQuiz()
    {
        if (!GameManager.Instance.isClearPuzzle4)
        {
        }
        OnQuizClear?.Invoke();
        //퀴즈 클리어시 사운드 효과
        SoundManager.Instance.PlaySoundOneShot("SuccessSound", 0.4f);
        puzzleCamera.transform.position = beforeCameraPos;
        puzzleCamera.transform.rotation = beforeCameraRot;
        gameObject.SetActive(false);
    }

    public void failQuiz()
    {
        isStart = false;
        failCanvas.SetActive(true);
        OnQuizFail?.Invoke();
    }
    
    public void retryQuiz()
    {
        failCanvas.SetActive(false);
        OnQuizRetry?.Invoke();
    }
}
