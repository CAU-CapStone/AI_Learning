using UnityEngine;

public class Npc4Interaction : Interaction
{
    private Quiz5 quiz5;

    void Start()
    {
        quiz5 = QuizDictionary.Instance.GetQuiz("Quiz5") as Quiz5;
    }
    
    public override void Activate()
    {
        if (!GameManager.Instance.isClearPuzzle5)
        {
            DialogueManager.Instance.onDialogueEnd.AddListener(StartQuiz5);
            DialogueManager.Instance.SetDialogue(59, 65);
        }
        else
        {
            DialogueManager.Instance.SetDialogue(33, 33);
        }
    }

    private void StartQuiz5()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(StartQuiz5);
        DialogueManager.Instance.SetDialogue(66, 67);
        quiz5.startQuiz();
        quiz5.OnQuizClear += ClearQuiz5;
        quiz5.OnQuizFail += FailQuiz5;
    }

    private void ClearQuiz5()
    {
        GameManager.Instance.isClearPuzzle5 = true;
        quiz5.OnQuizClear -= ClearQuiz5;
        quiz5.OnQuizFail -= FailQuiz5;
        GameManager.Instance.SetMainCamera();
        DialogueManager.Instance.onDialogueEnd.AddListener(EndDialogue);
        DialogueManager.Instance.SetDialogue(69, 69);
    }
    
    private void EndDialogue()
    {
        GameManager.Instance.npc4.SetActive(false);
    }

    private void FailQuiz5()
    {
        DialogueManager.Instance.SetDialogue(68, 68);
    }
}
