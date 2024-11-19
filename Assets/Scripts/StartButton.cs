using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    AudioSource buttonHoverMusic;
    AudioSource buttonClickMusic;
    
    RectTransform rect;

    float scaleMultiplier = 1.1f; // How much larger the button should get

    private void Start()
    {
        buttonHoverMusic = GameObject.Find("ButtonHoverMusic").GetComponent<AudioSource>();
        buttonClickMusic = GameObject.Find("ButtonClickMusic").GetComponent<AudioSource>();
        rect = GetComponent<RectTransform>();
    }

    public void StartGameButton()
    {
        buttonClickMusic.Play();
        transform.parent.gameObject.SetActive(false);
    }

    public void QuitGameButton()
    {
        buttonClickMusic.Play();
        Application.Quit();
    }

    public void OnPointerExit (PointerEventData eventData)
    {
        buttonHoverMusic.Play();
        StartCoroutine(ButtonHoverAnimation(rect, new Vector3(1, 1, 1), new Vector3(scaleMultiplier, scaleMultiplier, 1)));
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ButtonHoverAnimation(rect, new Vector3(scaleMultiplier, scaleMultiplier, 1), new Vector3(1, 1, 1)));
    }

    IEnumerator ButtonHoverAnimation(RectTransform button, Vector3 startScale, Vector3 endScale)
    {
        float time = 0;
        while (time < 0.2f)
        {
            time += Time.deltaTime;
            button.localScale = Vector3.Lerp(startScale, endScale, time);
            yield return null;
        }
    }
}
