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
        GameManager.Instance.SetPlayerAllowedToMove(false);
        closeButton.onClick.AddListener(CloseThis);
        closeButton.onClick.AddListener(StartLetterDialogue);
    }

    void CloseThis()
    {
        GameManager.Instance.SetPlayerAllowedToMove(true);
        this.GameObject().SetActive(false);
    }

    void StartLetterDialogue()
    {
        if (!GameManager.Instance.isClearPuzzle1)
        {
            DialogueManager.Instance.SetDialogue(5, 7);
        }
    }
}
