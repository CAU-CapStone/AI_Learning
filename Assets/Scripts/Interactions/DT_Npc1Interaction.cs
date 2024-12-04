public class DT_Npc1Interaction : Interaction
{
    private IQuiz dt_quiz1;
    private IQuiz dt_quiz2;
    
    void Start()
    {
        dt_quiz1 = QuizDictionary.Instance.GetQuiz("DTQuiz1");
        dt_quiz2 = QuizDictionary.Instance.GetQuiz("DTQuiz2");
    }
    
    public override void Activate()
    {
        if (!GameManager.Instance.dt_isClearPuzzle1)
        {
            GameManager.Instance.QuestTextSetActive(false);
            DialogueManager.Instance.onDialogueEnd.AddListener(StartPuzzle1);
            DialogueManager.Instance.SetDialogue(86, 88);
        }
        else if (!GameManager.Instance.dt_isClearPuzzle2)
        {
            GameManager.Instance.QuestTextSetActive(false);
            DialogueManager.Instance.onDialogueEnd.AddListener(StartPuzzle2);
            DialogueManager.Instance.SetDialogue(91, 92);
        }
        else
        {
            DialogueManager.Instance.SetDialogue(95, 95);
        }
    }
    
    private void StartPuzzle1()
    {
        //퀴즈 배경음악
        SoundManager.Instance.PlayQuiz();
        
        DialogueManager.Instance.onDialogueEnd.RemoveListener(StartPuzzle1);
        dt_quiz1.startQuiz();
        dt_quiz1.OnQuizClear += EndPuzzle1;
        DialogueManager.Instance.SetDialogue(89, 89);
    }
    
    private void EndPuzzle1()
    {
        //퀴즈 음악 정지
        SoundManager.Instance.StopQuiz();
        
        dt_quiz1.OnQuizClear -= EndPuzzle1;
        GameManager.Instance.dt_isClearPuzzle1 = true;
        GameManager.Instance.SetMainCamera();
        DialogueManager.Instance.onDialogueEnd.AddListener(EndNpc1Dialogue1);
        DialogueManager.Instance.SetDialogue(90, 90);
    }

    private void EndNpc1Dialogue1()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(EndNpc1Dialogue1);
        GameManager.Instance.SetQuestText("용사에게 뭔가 문제가 생긴 것 같다.\r\n말을 걸어보자");
    }

    private void StartPuzzle2()
    {
        //퀴즈 배경음악
        SoundManager.Instance.PlayQuiz();

        DialogueManager.Instance.onDialogueEnd.RemoveListener(StartPuzzle2);
        dt_quiz2.startQuiz();
        dt_quiz2.OnQuizClear += EndPuzzle2;
        DialogueManager.Instance.SetDialogue(93, 93);
    }
    
    private void EndPuzzle2()
    {
        //퀴즈 음악 정지
        SoundManager.Instance.StopQuiz();
        
        dt_quiz1.OnQuizClear -= EndPuzzle2;
        GameManager.Instance.dt_isClearPuzzle2 = true;
        GameManager.Instance.SetMainCamera();
        DialogueManager.Instance.onDialogueEnd.AddListener(EndNpc1Dialogue2);
        DialogueManager.Instance.SetDialogue(94, 94);
    }

    private void EndNpc1Dialogue2()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(EndNpc1Dialogue2);
        GameManager.Instance.SetQuestText("새로운 세계를 계속 탐험하자");
        
        GameManager.Instance.dt_npc1Portal.SetActive(false);
        GameManager.Instance.SetNpcLightBulbActive(GameManager.Instance.dt_npc1, false);
        
        GameManager.Instance.dt_npc2Portal.SetActive(true);
        GameManager.Instance.SetNpcLightBulbActive(GameManager.Instance.dt_npc2, true);
    }
}

