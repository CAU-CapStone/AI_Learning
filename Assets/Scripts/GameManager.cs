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
    public GameObject dt_pasture;
    
    [Header("positions")]
    public Transform startPosition;
    public Transform house4Npc2Position;
    public Transform dt_pastureNpc1Position;
    
    [Header("Cameras")]
    public Camera playerCamera;
    public Camera puzzleCamera;
    
    [Header("Game States")]
    public bool isReadBook = false;
    public bool isClearPuzzle1 = false;
    public bool isClearPuzzle2 = false;
    public bool isClearPuzzle3 = false;
    public bool isClearPuzzle4 = false;
    public bool isClearPuzzle5 = false;
    public bool dt_isClearPuzzle1 = false;
    public bool dt_isEndPuzzle1Dialogue = false;
    public bool dt_isClearPuzzle2 = false;
    
    public bool isAllowedToMove = false;
    
    private void Awake()
    {
        isAllowedToMove = false;
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
    
    public void StartGame()
    {
        isAllowedToMove = true;
        SetPlayerLocation(startPosition,false);
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

    public void SetGameObjectLocation(GameObject obj, Transform tf,bool isFade = false)
    {
	    if (isFade)
        {
            isAllowedToMove = false;
	        StartCoroutine(FadeAndTeleport(obj, tf));
	    }
	    else
        {
            obj.SetActive(false);
        	obj.transform.position = tf.position;
       		obj.transform.rotation = tf.rotation;
        	obj.SetActive(true);
        }
    }
    private IEnumerator FadeAndTeleport(GameObject obj, Transform tf)
    {
        isAllowedToMove = false;
        // 텔레포트 동안 화면 어둠게 하기
        yield return StartCoroutine(Fade(true));
        obj.SetActive(false);
        obj.transform.position = tf.position;
        obj.transform.rotation = tf.rotation;
        obj.SetActive(true);
        // 다시 화면 밝게 하기
        yield return new WaitForSeconds(1.70f);
        yield return StartCoroutine(Fade(false));
        isAllowedToMove = true;
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float portalFadeDuration = isFadeIn?0.4f:0.4f; // Time to fade in/out
        fadeImage.gameObject.SetActive(true);
        float targetAlpha = isFadeIn ? 1:0.1f;
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
            fadeImage.gameObject.SetActive(false);
        }
    }

    public void SetMainCamera()
    {
        puzzleCamera.enabled = false;
        playerCamera.enabled = true;
    }
}
