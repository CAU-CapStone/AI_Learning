using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

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
        nowDialogueId = startId;
        nowEndDialogueId = endId;
        canvasDialogue.enabled = true;
        ShowNextDialogue();
    }

    public void ShowNextDialogue()
    {
        if (nowDialogueId <= nowEndDialogueId)
        {
            speakerText.text = dialogues.dialogues[nowDialogueId].speaker;
            dialogueText.text = dialogues.dialogues[nowDialogueId].text;

            nowDialogueId++;
        }
        else
        {
            canvasDialogue.enabled = false;
            onDialogueEnd.Invoke();
        }
    }
}
