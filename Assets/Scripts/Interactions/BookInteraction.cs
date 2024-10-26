using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteraction : Interaction
{
    public GameObject letterCanvas;
    
    public override void Activate()
    {
        letterCanvas.SetActive(true);
        GameManager.Instance.isReadBook = true;
    }
}
