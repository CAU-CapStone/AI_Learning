using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTQuiz1 : MonoBehaviour, IQuiz
{
    public event Action OnQuizClear;
    public GameObject items;
    public Area animalArea;
    public Area toolArea;
    private int itemCount;
    public void startQuiz()
    {
        CameraManager.Instance.mainCamera.enabled = false;
        CameraManager.Instance.puzzleCamera.enabled= true;

        gameObject.SetActive(true);
    }

    private void endQuiz()
    {
        gameObject.SetActive(false);
        OnQuizClear?.Invoke();
    }

    private void Start()
    {
        itemCount = items.transform.childCount;
    }

    private void Update()
    {
        int animalAreaCount = animalArea.placedObjectList.Count;
        int toolAreaCount = toolArea.placedObjectList.Count;

        if(itemCount == animalAreaCount + toolAreaCount)
        {
            endQuiz();
        }
    }


}
