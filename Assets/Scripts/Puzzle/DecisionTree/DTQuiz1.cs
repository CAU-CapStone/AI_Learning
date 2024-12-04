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


    public ItemsPlacement itemsPlacement;
    public DTQuiz1UIHandler dtQuiz1UIHandler;
    public void startQuiz()
    {
        CameraManager.Instance.mainCamera.enabled = false;
        CameraManager.Instance.puzzleCamera.enabled= true;

        gameObject.SetActive(true);
    }

    public void endQuiz()
    {
        SoundManager.Instance.PlaySoundOneShot("SuccessSound", 0.4f);
        gameObject.SetActive(false);
        OnQuizClear?.Invoke();
    }

    private void Start()
    {
        itemCount = items.transform.childCount;
    }

    private void Update()
    {
    }

    public bool checkQuizResult()
    {
        int itemCount = items.transform.childCount;
        int placedObjectSum = 0;
        foreach (Area area in areaList)
        {
            placedObjectSum += area.placedObjectList.Count;
            if (!area.checkChildTag())
            {
                SoundManager.Instance.PlaySoundOneShot("Error",0.8f);
                return false;
            }
        }
        
        if (itemCount == placedObjectSum)
        {
            SoundManager.Instance.PlaySoundOneShot("quiz4",0.8f);
            return true;
        }
        
        SoundManager.Instance.PlaySoundOneShot("Error",0.8f);
        return false;
    }
    public void resetQuiz()
    {
        foreach (Area area in areaList)
        {
            area.clear();
        }
        itemsPlacement.ResetPositions();
        dtQuiz1UIHandler.resetUI();
    }
}
