using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject player;
    public Transform startPosition;

    public bool isReadBook = false;
    public bool isClearPuzzle1 = false;
    public bool isClearPuzzle2 = false;
    
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
        SetPlayerLocation(startPosition.position, startPosition.rotation,false);
    }
    
    public void SetPlayerAllowedToMove(bool isAllowed)
    {
        isAllowedToMove = isAllowed;
    }
    
    
    
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Image fadeImage;
    public void SetPlayerLocation(Vector3 position, Quaternion rotation,bool isFade = true)
    {
        if (isFade)
        {
            StartCoroutine(FadeAndTeleport(position, rotation));
        }
        else
        {
            isAllowedToMove = false;
            player.SetActive(false);
            player.transform.position = position;
            player.transform.rotation = rotation;
            player.SetActive(true);
            isAllowedToMove = true;
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
}
