using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public UnityEvent onDialogueEnd;

    private DialogueList dialogues;
    
    private Canvas canvasDialogue;
    private TextMeshProUGUI speakerText;
    private TextMeshProUGUI dialogueText;

    private int nowDialogueId;
    private int nowEndDialogueId;

    public Transform npc;

    public bool isDialogue = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            canvasDialogue = GetComponentInChildren<Canvas>();
            speakerText = canvasDialogue.GetComponentsInChildren<TextMeshProUGUI>()[0];
            dialogueText = canvasDialogue.GetComponentsInChildren<TextMeshProUGUI>()[1];
            canvasDialogue.enabled = false;
            
            string path = Path.Combine(Application.streamingAssetsPath, "DialogueJson.json");
            string json = File.ReadAllText(path);
            dialogues = JsonUtility.FromJson<DialogueList>(json);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SetDialogue(int startId, int endId)
    {
        GameManager.Instance.SetPlayerAllowedToMove(false);
        isDialogue = true;
        nowDialogueId = startId;
        nowEndDialogueId = endId;
        canvasDialogue.enabled = true;
        ShowNextDialogue();
    }
    
    public void ShowNextDialogue()
    {
        //만약 animator가 있는 npc면 interaction 실행
        if (npc != null)
        {
            Animator anim = npc.GetComponentInChildren<Animator>();
            if (anim != null)
            {
                Debug.Log("Play anim");
                anim.SetTrigger("Interaction");
            }
        }
        SoundManager.Instance.PlaySoundOneShot("DialogueButtonSound");
        if (nowDialogueId <= nowEndDialogueId)
        {
            speakerText.text = dialogues.dialogues[nowDialogueId].speaker;
            dialogueText.text = dialogues.dialogues[nowDialogueId].text;
            nowDialogueId++;
        }
        else
        {
            GameManager.Instance.SetPlayerAllowedToMove(true);
            isDialogue = false;
            canvasDialogue.enabled = false;
            onDialogueEnd.Invoke();
        }
    }
}
