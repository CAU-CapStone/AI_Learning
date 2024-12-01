using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemsPlacement : MonoBehaviour
{
    private Dictionary<Transform, Vector3> initialPositions = new Dictionary<Transform, Vector3>();

    void Awake()
    {
        foreach (Transform child in transform)
        {
            initialPositions[child] = child.position;
        }
    }

    public void ResetPositions()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Draggable");
            if (initialPositions.ContainsKey(child))
            {
                child.position = initialPositions[child];
            }
            else
            {
                Destroy(child.gameObject);
            }
        }
    }

}
