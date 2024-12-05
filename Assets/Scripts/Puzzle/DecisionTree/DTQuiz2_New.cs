using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DTQuiz2_New : MonoBehaviour, IQuiz
{
    public event Action OnQuizClear;
    public GameObject items;
    public List<Area_New> areaList = new();
    public int itemCount;
    
    public DTQuiz2UIHandler dtQuiz2UIHandler;
    
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
    
    public bool checkQuizResult()
    {
        int itemCount = items.transform.childCount;
        int placedObjectSum = 0;
        
        areaList.ForEach(area => placedObjectSum += area.placedObjectList.Count);
        
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
        dtQuiz2UIHandler.resetUI();
    }
}
