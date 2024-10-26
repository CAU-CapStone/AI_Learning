using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LetterControl : MonoBehaviour
{
    public Button closeButton;
    
    void Start()
    {
        closeButton.onClick.AddListener(CloseThis);
        closeButton.onClick.AddListener(StartLetterDialogue);
    }

    void CloseThis()
    {
        this.GameObject().SetActive(false);
    }

    void StartLetterDialogue()
    {
        DialogueManager.Instance.SetDialogue(5, 7);
    }
}
