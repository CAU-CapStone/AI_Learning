using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Area : MonoBehaviour
{
    public List<string> correctTag = new List<string>();
    public Transform topLeft;
    public Transform bottomRight;

    public List<GameObject> placedObjectList = new List<GameObject>();

    public int row;
    public int column;
    private Vector3 currentPosition;
    private float xSpacing;
    private float zSpacing;
    private int placedObjects = 0;


    private void Awake()
    {
        currentPosition = topLeft.position;
        xSpacing = Mathf.Abs(topLeft.position.x - bottomRight.position.x) / (column - 1);
        zSpacing = Mathf.Abs(topLeft.position.z - bottomRight.position.z) / (row - 1);
    }
    private void Start()
    {


    }

    public void init()
    {
        currentPosition = topLeft.position;
        xSpacing = Mathf.Abs(topLeft.position.x - bottomRight.position.x) / (column - 1);
        zSpacing = Mathf.Abs(topLeft.position.z - bottomRight.position.z) / (row - 1);
    }

    public bool TryPlaceObject(GameObject selectedObject)
    {
        //isCorrectTag 제거
        if (inArea(selectedObject.transform) && placedObjects < row * column)
        {
            placedObjectList.Add(selectedObject);
            placedObjects++;
            selectedObject.transform.position = currentPosition;
            UpdateNextPosition();
            return true;
        }
        return false;
    }

    public void placeObject(GameObject previousObject)
    {
        //isCorrectTag 제거
        if (placedObjects < row * column)
        {
            placedObjectList.Add(previousObject);
            placedObjects++;
            previousObject.transform.position = currentPosition;
            UpdateNextPosition();
        }
    }

    private bool inArea(Transform selectedObject)
    {
        if (selectedObject.position.x > topLeft.position.x &&
            selectedObject.position.x < bottomRight.position.x &&
            selectedObject.position.z < topLeft.position.z &&
            selectedObject.position.z > bottomRight.position.z)
        {
            return true;
        }
        return false;
    }

    private bool isCorrectTag(GameObject selectedObject)
    {
        foreach(string tag in correctTag)
        {
            if(selectedObject.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    public bool checkChildTag()
    {
        bool result = true;
        foreach(GameObject item in placedObjectList)
        {
            bool tagResult = false;
            foreach(string tag in correctTag)
            {
                if(item.CompareTag(tag))
                {
                    tagResult = true;
                }
            }
            result = result && tagResult;
        }

        return result;
    }

    private void UpdateNextPosition()
    {
        currentPosition.x += xSpacing;

        if (placedObjects % column == 0)
        {
            currentPosition.x = topLeft.position.x;
            currentPosition.z -= zSpacing;
        }
    }

    public void clear()
    {
        placedObjectList.Clear();
        placedObjects= 0;
        currentPosition = topLeft.transform.position;
    }
}
