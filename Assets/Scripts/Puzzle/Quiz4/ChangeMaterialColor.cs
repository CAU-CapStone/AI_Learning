using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialColor : MonoBehaviour
{
    public Color newColor;

    void Awake()
    {
        ChangeChildrenColor(newColor);
    }

    void ChangeChildrenColor(Color color)
    {
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in childRenderers)
        {
            renderer.material.color = color;
        }
    }
}
