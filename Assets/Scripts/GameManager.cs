using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject player;
    public Transform startPosition;

    public bool isReadBook = false;
    public bool isClearPuzzle1 = false;
    public bool isClearPuzzle2 = false;
    
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
        SetPlayerLocation(startPosition.position, startPosition.rotation);
    }

    public void SetPlayerLocation(Vector3 position, Quaternion rotation)
    {
        player.SetActive(false);
        player.transform.position = position;
        player.transform.rotation = rotation;
        player.SetActive(true);
    }
}
