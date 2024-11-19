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
            DialogueManager.Instance.onDialogueEnd.AddListener(EndLetterDialogue);
            DialogueManager.Instance.SetDialogue(5, 7);
        }
    }

    private void EndLetterDialogue()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(EndLetterDialogue);
        GameManager.Instance.SetQuestText("친구를 찾아 옆집으로 가자");
        GameManager.Instance.SetNpcLightBulbActive(GameManager.Instance.npc1, true);
            
        GameManager.Instance.frontBookPortal.SetActive(false);
        GameManager.Instance.house1InPortal.SetActive(true);
        GameManager.Instance.house2OutPortal.SetActive(true);
        GameManager.Instance.npc1Portal.SetActive(true);
    }
}
