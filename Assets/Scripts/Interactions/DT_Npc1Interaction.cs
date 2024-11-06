using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_Npc1Interaction : Interaction
{
    private IQuiz dt_quiz1;
    
    void Start()
    {
        dt_quiz1 = QuizDictionary.Instance.GetQuiz("DTQuiz1");
    }
    
    public override void Activate()
    {
        if (!GameManager.Instance.dt_isClearPuzzle1)
        {
            DialogueManager.Instance.onDialogueEnd.AddListener(StartPuzzle);
            DialogueManager.Instance.SetDialogue(34, 36);
        }
        else if (!GameManager.Instance.dt_isEndPuzzle1Dialogue)
        {
            DialogueManager.Instance.SetDialogue(41, 41);
        }
        else
        {
            DialogueManager.Instance.SetDialogue(33, 33);
        }
    }
    
    private void StartPuzzle()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(StartPuzzle);
        dt_quiz1.startQuiz();
        dt_quiz1.OnQuizClear += EndPuzzle;
        DialogueManager.Instance.SetDialogue(37, 37);
    }
    
    private void EndPuzzle()
    {
        dt_quiz1.OnQuizClear -= EndPuzzle;
        GameManager.Instance.dt_isClearPuzzle1 = true;
        GameManager.Instance.SetMainCamera();
        DialogueManager.Instance.onDialogueEnd.AddListener(EndNpc1Dialogue);
        DialogueManager.Instance.SetDialogue(39, 40);
    }

    private void EndNpc1Dialogue()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(EndNpc1Dialogue);
    }
}

