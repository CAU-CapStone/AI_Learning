using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    /*public AudioSource buttonHoverSound;
    public AudioSource buttonClickSound;
    
    RectTransform startButton;
    RectTransform quitButton;

    public float scaleMultiplier = 1.2f; // How much larger the button should get

    private void Start()
    {
        startButton = transform.Find("StartButton").GetComponent<RectTransform>();
        quitButton = transform.Find("QuitButton").GetComponent<RectTransform>();
    }

    public void StartGameButton()
    {
        buttonClickSound.Play();
        gameObject.SetActive(false);
    }

    public void QuitGameButton()
    {
        
        buttonClickSound.Play();
        Application.Quit();
    }

    public void StartButtonHover()
    {
        buttonHoverSound.Play();
        StartCoroutine(ButtonHoverAnimation(startButton, new Vector3(1, 1, 1), new Vector3(scaleMultiplier, scaleMultiplier, 1)));
    }
    
    public void StartButtonExit()
    {
        StartCoroutine(ButtonHoverAnimation(startButton, new Vector3(scaleMultiplier, scaleMultiplier, 1), new Vector3(1, 1, 1)));
    }
    
    public void QuitButtonHover()
    {
        buttonHoverSound.Play();
        StartCoroutine(ButtonHoverAnimation(quitButton, new Vector3(1, 1, 1), new Vector3(scaleMultiplier, scaleMultiplier, 1)));
    }

    public void QuitButtonExit()
    {
        StartCoroutine(ButtonHoverAnimation(quitButton, new Vector3(scaleMultiplier, scaleMultiplier, 1), new Vector3(1, 1, 1)));
    }
    
    IEnumerator ButtonHoverAnimation(RectTransform button, Vector3 startScale, Vector3 endScale)
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            button.localScale = Vector3.Lerp(startScale, endScale, time);
            yield return null;
        }
    }*/
}
