using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Area_New : MonoBehaviour
{
    public List<string> correctTag = new List<string>();
   
    public List<GameObject> placedObjectList = new List<GameObject>();
    
    private Vector3 currentPosition;
    private float xSpacing;
    private float zSpacing;
    private int placedObjects = 0;


    private void Awake()
    {
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (correctTag.Contains(other.tag))
            placedObjectList.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        placedObjectList.Remove(other.gameObject);
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

    public void clear()
    {
        placedObjectList.Clear();
        placedObjects = 0;
    }
}
