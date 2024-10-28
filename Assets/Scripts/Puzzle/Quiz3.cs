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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
