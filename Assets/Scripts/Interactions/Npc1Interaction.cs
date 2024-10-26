using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc1Interaction : Interaction
{
    public Quiz1 quiz1;
    
    public override void Activate()
    {
        DialogueManager.Instance.onDialogueEnd.AddListener(StartPuzzle);
        DialogueManager.Instance.SetDialogue(2, 4);
        
    }

    private void StartPuzzle()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(StartPuzzle);
        quiz1.startQuiz();
    }
}
