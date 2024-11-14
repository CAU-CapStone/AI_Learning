public class DT_PastureInteraction : Interaction
{
    private IQuiz dt_quiz2;

    void Start()
    {
        dt_quiz2 = QuizDictionary.Instance.GetQuiz("DTQuiz2");
    }
    
    public override void Activate()
    {
        base.Activate();
        if (GameManager.Instance.dt_isClearPuzzle1 && !GameManager.Instance.dt_isClearPuzzle2)
        {
            DialogueManager.Instance.onDialogueEnd.AddListener(EndActivate);
        }
        DialogueManager.Instance.SetDialogue(42, 42);
    }

    private void EndActivate()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(EndActivate);
        GameManager.Instance.dt_isEndPuzzle1Dialogue = true;
        GameManager.Instance.SetGameObjectLocation(GameManager.Instance.npc1, GameManager.Instance.dt_pastureNpc1Position);
        
        DialogueManager.Instance.onDialogueEnd.AddListener(EndActivateDialogue);
        DialogueManager.Instance.SetDialogue(43, 45);
    }
    
    private void EndActivateDialogue()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(EndActivateDialogue);
        dt_quiz2.startQuiz();
        dt_quiz2.OnQuizClear += EndPuzzle;
        DialogueManager.Instance.SetDialogue(46, 47);
    }

    private void EndPuzzle()
    {
        dt_quiz2.OnQuizClear -= EndPuzzle;
        GameManager.Instance.dt_isClearPuzzle2 = true;
        GameManager.Instance.SetMainCamera();
        DialogueManager.Instance.SetDialogue(48, 49);
    }
}
