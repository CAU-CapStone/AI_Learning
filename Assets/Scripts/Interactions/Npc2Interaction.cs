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
            GameManager.Instance.QuestTextSetActive(false);
            DialogueManager.Instance.onDialogueEnd.AddListener(StartPuzzle3);
            DialogueManager.Instance.SetDialogue(25, 26);
        }
        else if (GameManager.Instance.isClearPuzzle4 && GameManager.Instance.isClearPuzzle5)
        {
            GameManager.Instance.QuestTextSetActive(false);
            GameManager.Instance.SetNpcLightBulbActive(GameManager.Instance.npc2, false);
            DialogueManager.Instance.onDialogueEnd.AddListener(EndKnnDialogue);
            DialogueManager.Instance.SetDialogue(82, 85);
        }
        else
        {
            DialogueManager.Instance.SetDialogue(32, 32);
        }
    }
    
    private void StartPuzzle3()
    {
        //퀴즈 배경음악
        SoundManager.Instance.PlayQuiz();
        
        DialogueManager.Instance.onDialogueEnd.RemoveListener(StartPuzzle3);
        DialogueManager.Instance.SetDialogue(81, 81);
        quiz3.startQuiz();
        quiz3.OnQuizClear += EndPuzzle3;
    }

    private void EndPuzzle3()
    {
        
        //퀴즈 음악 정지
        SoundManager.Instance.StopQuiz();
        
        quiz3.OnQuizClear -= EndPuzzle3;
        GameManager.Instance.isClearPuzzle3 = true;
        GameManager.Instance.SetMainCamera();
        DialogueManager.Instance.onDialogueEnd.AddListener(EndPuzzle3Dialogue);
        DialogueManager.Instance.SetDialogue(28, 31);
    }

    private void EndPuzzle3Dialogue()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(EndPuzzle3Dialogue);
        GameManager.Instance.SetQuestText("밖으로 나가 도움이 필요한 사람을 찾자");
        GameManager.Instance.npc3.SetActive(true);
        GameManager.Instance.npc4.SetActive(true);
        GameManager.Instance.SetNpcLightBulbActive(GameManager.Instance.npc2, false);
        GameManager.Instance.SetNpcLightBulbActive(GameManager.Instance.npc3, true);
        GameManager.Instance.SetNpcLightBulbActive(GameManager.Instance.npc4, true);
        
        GameManager.Instance.house4OutPortal.SetActive(false);
        GameManager.Instance.npc2Portal.SetActive(false);
        
        GameManager.Instance.house4InPortal.SetActive(true);
        GameManager.Instance.npc3Portal.SetActive(true);
        GameManager.Instance.npc4Portal.SetActive(true);
    }

    private void EndKnnDialogue()
    {
        DialogueManager.Instance.onDialogueEnd.RemoveListener(EndKnnDialogue);
        GameManager.Instance.SetQuestText("마을을 나가자");
        GameManager.Instance.npc2Portal2.SetActive(false);
        GameManager.Instance.knnPortal.SetActive(true);
        GameManager.Instance.isClearKnn = true;
    }
}
