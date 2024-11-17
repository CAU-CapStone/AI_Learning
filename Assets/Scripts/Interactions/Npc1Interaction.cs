using UnityEngine;

public class Npc1Interaction : Interaction
{
    private IQuiz quiz1;
    private IQuiz quiz2;

    void Start()
    {
        quiz1 = QuizDictionary.Instance.GetQuiz("Quiz1");
        quiz2 = QuizDictionary.Instance.GetQuiz("Quiz2");
    }
    
    public override void Activate()
    {
        if (!GameManager.Instance.isClearPuzzle1)
        {
            DialogueManager.Instance.onDialogueEnd.AddListener(StartPuzzle1);
            DialogueManager.Instance.SetDialogue(2, 4);
        }
        else if (!GameManager.Instance.isClearPuzzle2)
        {
            DialogueManager.Instance.onDialogueEnd.AddListener(StartPuzzle2);
            DialogueManager.Instance.SetDialogue(74, 75);
        }
        else
        {
            DialogueManager.Instance.SetDialogue(24, 24);
        }
    }

    private void StartPuzzle1()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(StartPuzzle1);
        quiz1.startQuiz();
        quiz1.OnQuizClear += EndPuzzle1;
        DialogueManager.Instance.SetDialogue(20, 20);
    }

    private void EndPuzzle1()
    {
        GameManager.Instance.SetMagicCirclePos(new Vector3(21.6f,-1.428f, 501.84f));
        GameManager.Instance.SetMagicCircleOutPos(new Vector3(61.22f,22.2f,27.82f));
        quiz1.OnQuizClear -= EndPuzzle1;
        GameManager.Instance.isClearPuzzle1 = true;
        GameManager.Instance.SetMainCamera();
        DialogueManager.Instance.onDialogueEnd.AddListener(EndNpc1Dialogue1);
        DialogueManager.Instance.SetDialogue(70, 73);
    }

    private void EndNpc1Dialogue1()
    {
        GameManager.Instance.SetQuestText("친구에게 다시 말을 걸어보자");
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
        DialogueManager.Instance.onDialogueEnd.AddListener(EndNpc1Dialogue2);
        DialogueManager.Instance.SetDialogue(17, 19);
    }

    private void EndNpc1Dialogue2()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(EndNpc1Dialogue2);
        DialogueManager.Instance.SetDialogue(76, 77);
    }
}
