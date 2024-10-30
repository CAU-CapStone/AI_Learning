using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Player")]
    public GameObject player;
    
    [Header("NPCs")]
    public GameObject npc1;
    public GameObject npc2;
    public GameObject npc3;
    public GameObject npc4;
    
    [Header("positions")]
    public Transform startPosition;
    public Transform house4Npc2Position;
    
    [Header("Cameras")]
    public Camera playerCamera;
    public Camera puzzleCamera;
    
    [Header("Game States")]
    public bool isReadBook = false;
    public bool isClearPuzzle1 = false;
    public bool isClearPuzzle2 = false;
    public bool isClearPuzzle3 = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        SetPlayerLocation(startPosition);
    }

    public void SetPlayerLocation(Transform tf)
    {
        SetGameObjectLocation(player, tf);
    }

    public static void SetGameObjectLocation(GameObject obj, Transform tf)
    {
        obj.SetActive(false);
        obj.transform.position = tf.position;
        obj.transform.rotation = tf.rotation;
        obj.SetActive(true);
    }

    public void SetMainCamera()
    {
        puzzleCamera.enabled = false;
        playerCamera.enabled = true;
    }
}
