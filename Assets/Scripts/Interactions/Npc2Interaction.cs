using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc2Interaction : Interaction
{
    private IQuiz quiz2;
    private IQuiz quiz3;
    
    void Start()
    {
        quiz2 = QuizDictionary.Instance.GetQuiz("Quiz2");
        quiz3 = QuizDictionary.Instance.GetQuiz("Quiz3");
    }
    
    public override void Activate()
    {
        if (!GameManager.Instance.isClearPuzzle2)
        {
            DialogueManager.Instance.onDialogueEnd.AddListener(StartPuzzle2);
            DialogueManager.Instance.SetDialogue(15, 15);
        }
        else if (!GameManager.Instance.isClearPuzzle3)
        {
            DialogueManager.Instance.onDialogueEnd.AddListener(StartPuzzle3);
            DialogueManager.Instance.SetDialogue(25, 26);
        }
        else
        {
            DialogueManager.Instance.SetDialogue(32, 32);
        }
    }
    
    private void StartPuzzle2()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(StartPuzzle2);
        DialogueManager.Instance.SetDialogue(21, 21);
        quiz2.startQuiz();
        quiz2.OnQuizClear += EndPuzzle2;
    }

    private void EndPuzzle2()
    {
        quiz2.OnQuizClear -= EndPuzzle2;
        GameManager.Instance.isClearPuzzle2 = true;
        GameManager.Instance.SetMainCamera();
        DialogueManager.Instance.SetDialogue(16, 19);
        DialogueManager.Instance.onDialogueEnd.AddListener(EndPuzzle2Dialogue);
        DialogueManager.Instance.SetDialogue(22, 23);
    }

    private void EndPuzzle2Dialogue()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(EndPuzzle2Dialogue);
        
        GameManager.SetGameObjectLocation(GameManager.Instance.npc2, GameManager.Instance.house4Npc2Position);
    }

    private void StartPuzzle3()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(StartPuzzle3);
        DialogueManager.Instance.SetDialogue(27, 27);
        quiz3.startQuiz();
        quiz3.OnQuizClear += EndPuzzle3;
    }

    private void EndPuzzle3()
    {
        quiz3.OnQuizClear -= EndPuzzle3;
        GameManager.Instance.isClearPuzzle3 = true;
        GameManager.Instance.SetMainCamera();
        DialogueManager.Instance.onDialogueEnd.AddListener(EndPuzzle3Dialogue);
        DialogueManager.Instance.SetDialogue(28, 31);
    }

    private void EndPuzzle3Dialogue()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(EndPuzzle3Dialogue);
        GameManager.Instance.npc3.SetActive(true);
        GameManager.Instance.npc4.SetActive(true);
    }
}
