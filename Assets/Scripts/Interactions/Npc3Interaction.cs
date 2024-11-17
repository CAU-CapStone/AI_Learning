public class Npc3Interaction : Interaction
{
    private IQuiz quiz4;

    void Start()
    {
        quiz4 = QuizDictionary.Instance.GetQuiz("Quiz4");
    }
    
    public override void Activate()
    {
        if (!GameManager.Instance.isClearPuzzle4)
        {
            GameManager.Instance.QuestTextSetActive(false);
            DialogueManager.Instance.onDialogueEnd.AddListener(StartPuzzle4);
            DialogueManager.Instance.SetDialogue(50, 55);
        }
        else
        {
            DialogueManager.Instance.SetDialogue(58, 58);
        }
    }

    private void StartPuzzle4()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(StartPuzzle4);
        DialogueManager.Instance.SetDialogue(56, 56);
        quiz4.startQuiz();
        quiz4.OnQuizClear += EndPuzzle4;
    }

    private void EndPuzzle4()
    {
        GameManager.Instance.isClearPuzzle4 = true;
        quiz4.OnQuizClear -= EndPuzzle4;
        GameManager.Instance.SetMainCamera();
        DialogueManager.Instance.SetDialogue(57, 57);
        DialogueManager.Instance.onDialogueEnd.AddListener(EndDialogue);
    }

    private void EndDialogue()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(EndDialogue);
        GameManager.Instance.SetQuestText("도울 사람을 찾아보자");
    }
}
