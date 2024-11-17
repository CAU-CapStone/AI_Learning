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
        GameManager.Instance.SetMagicCirclePos(new Vector3(21.6f,-1.428f, 501.84f));
        GameManager.Instance.SetMagicCircleOutPos(new Vector3(61.22f,22.2f,27.82f));
        GameManager.Instance.SetQuestText("바로 옆에 있는 장로님께 말을 걸어보자");
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
