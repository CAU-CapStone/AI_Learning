using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc2Interaction : Interaction
{
    public Quiz1 quiz2;
    
    public Camera playerCamera;
    public Camera puzzleCamera;
    
    public override void Activate()
    {
        DialogueManager.Instance.onDialogueEnd.AddListener(StartPuzzle);
        DialogueManager.Instance.SetDialogue(15, 15);
    }
    
    private void StartPuzzle()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(StartPuzzle);
        DialogueManager.Instance.SetDialogue(21, 21);
        quiz2.startQuiz();
        quiz2.OnQuizClear += EndPuzzle;
    }

    private void EndPuzzle()
    {
        quiz2.OnQuizClear -= EndPuzzle;
        GameManager.Instance.isClearPuzzle2 = true;
        puzzleCamera.enabled = false;
        playerCamera.enabled = true;
        DialogueManager.Instance.SetDialogue(16, 19);
    }
}
