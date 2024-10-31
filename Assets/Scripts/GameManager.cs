using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

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
    
    public bool isAllowedToMove = true;
    
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
        SetPlayerLocation(startPosition.position,false);
    }
    
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Image fadeImage;
    public void SetPlayerLocation(Transform tf, bool isFade = false)
    {
        SetGameObjectLocation(player, tf,isFade);
    }
    
    public void SetPlayerAllowedToMove(bool isAllowed)
    {
        isAllowedToMove = isAllowed;
    }

    public static void SetGameObjectLocation(GameObject obj, Transform tf,bool isFade)
    {
	    if (isFade)
	    {
	        StartCoroutine(FadeAndTeleport(GameObject obj, Transform tfn));
	    }
	    else
        {
            obj.SetActive(false);
        	obj.transform.position = tf.position;
       		obj.transform.rotation = tf.rotation;
        	obj.SetActive(true);
        }
    }
    private IEnumerator FadeAndTeleport(Vector3 position, Quaternion rotation)
    {
        isAllowedToMove = false;
        // 텔레포트 동안 화면 어둠게 하기
        yield return StartCoroutine(Fade(true));
        player.SetActive(false);
        player.transform.position = position;
        player.transform.rotation = rotation;
        player.SetActive(true);
        // 다시 화면 밝게 하기
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(Fade(false));
        isAllowedToMove = true;
        
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float portalFadeDuration = isFadeIn?0.4f:1.2f; // Time to fade in/out
        fadeImage.enabled = true;
        float targetAlpha = isFadeIn ? 1:0;
        float startAlpha = isFadeIn ? 0:1;
        Color color = fadeImage.color;
        float timer = 0;

        while (timer < portalFadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, timer / portalFadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        fadeImage.color = color;
        if (!isFadeIn)
        {
            fadeImage.enabled = false;
        }
    }
    
    GameObject obj, Transform tf

    public void SetMainCamera()
    {
        puzzleCamera.enabled = false;
        playerCamera.enabled = true;
    }
}
