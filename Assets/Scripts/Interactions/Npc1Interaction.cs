using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc1Interaction : Interaction
{
    public Quiz1 quiz1;
    
    public Camera playerCamera;
    public Camera puzzleCamera;

    public GameObject npc2;
    public GameObject npc2Trigger;
    
    public override void Activate()
    {
        if (!GameManager.Instance.isClearPuzzle1)
        {
            DialogueManager.Instance.onDialogueEnd.AddListener(StartPuzzle);
            DialogueManager.Instance.SetDialogue(2, 4);
        }
        else
        {
            DialogueManager.Instance.SetDialogue(14, 14);
        }
    }

    private void StartPuzzle()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(StartPuzzle);
        quiz1.startQuiz();
        quiz1.OnQuizClear += EndPuzzle;
        DialogueManager.Instance.SetDialogue(20, 20);
    }

    private void EndPuzzle()
    {
        quiz1.OnQuizClear -= EndPuzzle;
        GameManager.Instance.isClearPuzzle1 = true;
        puzzleCamera.enabled = false;
        playerCamera.enabled = true;
        DialogueManager.Instance.onDialogueEnd.AddListener(EndNpc1Dialogue);
        DialogueManager.Instance.SetDialogue(9, 10);
    }

    private void EndNpc1Dialogue()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(EndNpc1Dialogue);
        npc2.SetActive(true);
        npc2Trigger.SetActive(true);
        DialogueManager.Instance.SetDialogue(11, 13);
    }
}
