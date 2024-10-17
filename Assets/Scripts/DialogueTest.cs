using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTest : MonoBehaviour
{
    public Button startBtn;
    public Button nextBtn;
    
    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.Instance.onDialogueEnd.AddListener(() => Debug.Log("End Dialogue"));
        startBtn.onClick.AddListener(() =>
        {
            DialogueManager.Instance.SetDialogue(2, 4);
            DialogueManager.Instance.ShowNextDialogue();
        });
        
        nextBtn.onClick.AddListener(() =>
        {
            DialogueManager.Instance.ShowNextDialogue();
        });
    }
}
