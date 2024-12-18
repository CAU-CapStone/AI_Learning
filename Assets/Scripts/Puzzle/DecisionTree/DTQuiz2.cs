using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DTQuiz2 : MonoBehaviour, IQuiz
{
    public event Action OnQuizClear;
    public GameObject items;
    public List<Area> areaList = new List<Area>();
    public int itemCount;

    public Area dtQuiz1ToolArea;
    public Area dtQuiz1AnimalArea;
    public Area toolArea;
    public Area catArea;

    public ItemsPlacement itemsPlacement;
    public DTQuiz2UIHandler dtQuiz2UIHandler;
    public void startQuiz()
    {
        CameraManager.Instance.mainCamera.enabled = false;
        CameraManager.Instance.puzzleCamera.enabled = true;

        gameObject.SetActive(true);
        placeDTQuiz1Object();
    }

    private void placeDTQuiz1Object()
    {
        foreach(GameObject tool in dtQuiz1ToolArea.placedObjectList)
        {
            GameObject newTool = Instantiate(tool,items.transform);
            newTool.layer = LayerMask.NameToLayer("Default");
            toolArea.placeObject(newTool);
        }

        foreach(GameObject animal in dtQuiz1AnimalArea.placedObjectList)
        {
            GameObject newAnimal = Instantiate(animal,items.transform);
            newAnimal.layer = LayerMask.NameToLayer("Draggable");
            //catArea.placeObject(animal);
        }
    }

    public void endQuiz()
    {
        SoundManager.Instance.PlaySoundOneShot("SuccessSound", 0.4f);
        gameObject.SetActive(false);
        OnQuizClear?.Invoke();
    }

    private void Start()
    {
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
        placeDTQuiz1Object();
        dtQuiz2UIHandler.resetUI();
    }
}
