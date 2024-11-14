using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz4House : MonoBehaviour
{
    public Quiz4UIHandler quiz4UIHandler;
    public FindNearestHouse FindNearestHouse;

    private Color currentColor;
    void Start()
    {
        ChangeColor(Color.white);
    }

    void Update()
    {
        
    }

    private void ChangeColor(Color color)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
        currentColor= color;
    }

    public void submitAnswerColor(Color color)
    {
        ChangeColor(color);
        FindNearestHouse.checkAnswerColor(color);
    }

    private void OnMouseDown()
    {
        quiz4UIHandler.showColorButton();
    }
}
