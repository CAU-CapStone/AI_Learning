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
            GameManager.Instance.QuestTextSetActive(false);
            DialogueManager.Instance.onDialogueEnd.AddListener(StartQuiz5);
            DialogueManager.Instance.SetDialogue(59, 65);
        }
    }

    private void StartQuiz5()
    {
        //퀴즈 배경음악
        SoundManager.Instance.PlayQuiz();
        
        DialogueManager.Instance.onDialogueEnd.RemoveListener(StartQuiz5);
        DialogueManager.Instance.SetDialogue(66, 67);
        quiz5.startQuiz();
        quiz5.OnQuizClear += ClearQuiz5;
        quiz5.OnQuizFail += FailQuiz5;
    }

    private void ClearQuiz5()
    {
        //퀴즈 음악 정지
        SoundManager.Instance.StopQuiz();
        
        GameManager.Instance.isClearPuzzle5 = true;
        quiz5.OnQuizClear -= ClearQuiz5;
        quiz5.OnQuizFail -= FailQuiz5;
        GameManager.Instance.SetMainCamera();
        DialogueManager.Instance.onDialogueEnd.AddListener(EndDialogue);
        DialogueManager.Instance.SetDialogue(69, 69);
    }
    
    private void EndDialogue()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(EndDialogue);
        GameManager.Instance.SetNpcLightBulbActive(GameManager.Instance.npc4, false);
        GameManager.Instance.npc4.SetActive(false);
        GameManager.Instance.npc4Portal.SetActive(false);
        if (!GameManager.Instance.isClearPuzzle4)
        {
            GameManager.Instance.SetQuestText("도울 사람을 찾아보자");
        }
        else
        {
            GameManager.Instance.SetGameObjectLocation(GameManager.Instance.npc2, GameManager.Instance.npc2TeleportPosition);
            GameManager.Instance.SetNpcLightBulbActive(GameManager.Instance.npc2, true);
            GameManager.Instance.npc2Portal2.SetActive(true);
            GameManager.Instance.SetQuestText("장로님이 밖으로 나오신 것 같다.\r\n말을 걸어보자");
        }
    }

    private void FailQuiz5()
    {
        DialogueManager.Instance.SetDialogue(68, 68);
    }
}
