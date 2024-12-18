using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneClickHandler : MonoBehaviour
{
    public bool isAnswer = false;
    private NearestStoneFind nearestStoneFind;
    private Color originalColor;

    private void Start()
    {
        nearestStoneFind=GetComponentInParent<NearestStoneFind>();
        originalColor=GetComponent<Renderer>().material.color;
    }

    private void OnMouseDown()
    {
        //찍 소리 내기
        SoundManager.Instance.PlaySoundOneShot("Squish", 0.9f);
        
        if(isAnswer)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.color= Color.green;
            isAnswer= false;
            nearestStoneFind.NearestStoneClicked();
        }
    }

    public void ResetColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        if(renderer.material.color != originalColor)
        {
            renderer.material.color= originalColor;
        }
    }
}
