using UnityEngine;

public class BookInteraction : Interaction
{
    public GameObject letterCanvas;
    
    public override void Activate()
    {
        letterCanvas.SetActive(true);
        GameManager.Instance.isReadBook = true;
        GameManager.Instance.QuestTextSetActive(false);
    }
}
