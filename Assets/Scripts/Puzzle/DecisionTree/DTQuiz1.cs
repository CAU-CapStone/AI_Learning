using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTQuiz1 : MonoBehaviour, IQuiz
{
    public event Action OnQuizClear;
    public GameObject items;
    public List<Area> areaList = new List<Area>();
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
        int placedObjectSum = 0;
        foreach (Area area in areaList)
        {
            placedObjectSum += area.placedObjectList.Count;
        }

        if (itemCount == placedObjectSum)
        {
            endQuiz();
        }

        
    }


}
