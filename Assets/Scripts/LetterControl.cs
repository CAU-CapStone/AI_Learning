using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LetterControl : Interaction
{
    public Button closeButton;
    
    void Start()
    {
        GameManager.Instance.SetPlayerAllowedToMove(false);
        closeButton.onClick.AddListener(Activate);
        closeButton.onClick.AddListener(StartLetterDialogue);
    }

    public override void Activate()
    {
        base.Activate();
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
