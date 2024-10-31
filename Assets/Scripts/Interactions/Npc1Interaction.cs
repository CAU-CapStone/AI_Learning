using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc1Interaction : Interaction
{
    private IQuiz quiz1;

    void Start()
    {
        quiz1 = QuizDictionary.Instance.GetQuiz("Quiz1");
    }
    
    public override void Activate()
    {
        if (!GameManager.Instance.isClearPuzzle1)
        {
            DialogueManager.Instance.onDialogueEnd.AddListener(StartPuzzle);
            DialogueManager.Instance.SetDialogue(2, 4);
        }
        else if (!GameManager.Instance.isClearPuzzle2)
        {
            DialogueManager.Instance.SetDialogue(14, 14);
        }
        else
        {
            DialogueManager.Instance.SetDialogue(24, 24);
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
        GameManager.Instance.SetMainCamera();
        DialogueManager.Instance.onDialogueEnd.AddListener(EndNpc1Dialogue);
        DialogueManager.Instance.SetDialogue(9, 10);
    }

    private void EndNpc1Dialogue()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(EndNpc1Dialogue);
        GameManager.Instance.npc2.SetActive(true);
        DialogueManager.Instance.SetDialogue(11, 13);
    }
}
