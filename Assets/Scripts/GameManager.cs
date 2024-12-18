using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
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
    public GameObject dt_npc1;
    public GameObject dt_npc2;
    public GameObject dt_pasture;
    
    [Header("positions")]
    public Transform startPosition;
    public Transform npc2TeleportPosition;
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
    public bool isClearKnn = false;
    public bool dt_isEndPuzzle1Dialogue = false;
    public bool dt_isClearPuzzle1 = false;
    public bool dt_isClearPuzzle2 = false;

    [Header("Portal Effects")]
    public GameObject frontBookPortal;
    public GameObject house1InPortal;
    public GameObject house2InPortal;
    public GameObject house2OutPortal;
    public GameObject house4InPortal;
    public GameObject house4OutPortal;
    public GameObject npc1Portal;
    public GameObject npc2Portal;
    public GameObject npc3Portal;
    public GameObject npc4Portal;
    public GameObject npc2Portal2;
    public GameObject knnPortal;
    public GameObject dt_npc1Portal;
    public GameObject dt_npc2Portal;
    
    public bool isAllowedToMove = false;

    public TMP_Text quest;
    public GameObject portal;

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

    IEnumerator Start()
    {
        SoundManager.Instance.PlaySoundOneShot("Warp");
        SetPlayerLocation(startPosition,false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Fade(false));
        yield return new WaitForSeconds(0.7f);
        isAllowedToMove = true;
        SetQuestText("책상 위에 편지를 읽어보자");
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
        quest.transform.parent.gameObject.SetActive(false);
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
        quest.transform.parent.gameObject.SetActive(true);
        isAllowedToMove = true;
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float portalFadeDuration = isFadeIn?0.4f:0.4f; // Time to fade in/out
        fadeImage.gameObject.SetActive(true);
        float targetAlpha = isFadeIn ? 1:0f;
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

    public void SetQuestText(string str)
    {
        quest.transform.parent.gameObject.SetActive(true);
        quest.text = str;
    }
    
    public void QuestTextSetActive(bool b)
    {
        quest.transform.parent.gameObject.SetActive(b);
    }

    public void SetNpcLightBulbActive(GameObject npc, bool b)
    {
        npc.transform.Find("LightBulb").gameObject.SetActive(b);
    }
    
    //플레이어가 transform t를 바라보게 함
    public void PlayerLooksAt(Transform t)
    {
        player.transform.Rotate(0, Vector3.SignedAngle(player.transform.forward, t.position - player.transform.position, Vector3.up), 0);
    }
}
