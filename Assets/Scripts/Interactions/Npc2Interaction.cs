using UnityEngine;

public class Npc2Interaction : Interaction
{
    private IQuiz quiz3;
    
    void Start()
    {
        quiz3 = QuizDictionary.Instance.GetQuiz("Quiz3");
    }
    
    public override void Activate()
    {
        if (!GameManager.Instance.isClearPuzzle3)
        {
            DialogueManager.Instance.onDialogueEnd.AddListener(StartPuzzle3);
            DialogueManager.Instance.SetDialogue(25, 26);
        }
        else
        {
            DialogueManager.Instance.SetDialogue(32, 32);
        }
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
