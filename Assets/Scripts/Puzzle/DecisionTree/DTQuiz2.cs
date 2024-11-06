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
            newTool.layer = LayerMask.NameToLayer("Draggable");
            toolArea.placeObject(newTool);
        }

        foreach(GameObject animal in dtQuiz1AnimalArea.placedObjectList)
        {
            GameObject newAnimal = Instantiate(animal,items.transform);
            newAnimal.layer = LayerMask.NameToLayer("Draggable");
        }
    }

    private void endQuiz()
    {
        gameObject.SetActive(false);
        OnQuizClear?.Invoke();
    }

    private void Start()
    {
    }

    private void Update()
    {
        int itemCount = items.transform.childCount;
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
