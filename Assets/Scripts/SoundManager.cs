using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private Dictionary<string, AudioClip> audioClipCache;
    private AudioSource audioSource;

    private AudioSource quizMusic;
    private AudioSource backgroundMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioClipCache = new Dictionary<string, AudioClip>();
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        var go = GameObject.Find("BackgroundMusicPlayer");
        if (go != null)
        {
            backgroundMusic = go.transform.GetChild(0).GetComponent<AudioSource>();
            quizMusic = go.transform.GetChild(1).GetComponent<AudioSource>();
        }
    }

    public AudioClip GetAudioClip(string soundName)
    {
        if (!audioClipCache.TryGetValue(soundName, out AudioClip clip))
        {
            clip = Resources.Load<AudioClip>("Sounds/" + soundName);
            if (clip != null)
            {
                audioClipCache[soundName] = clip;
            }
            else
            {
                Debug.LogWarning("Sound not found: " + soundName);
            }
        }
        return clip;
    }

    public void PlaySoundOneShot(string soundName, float volume = 1f)
    {
        AudioClip clip = GetAudioClip(soundName);
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }

    public void PlayQuiz()
    {
        float fadeDuration = 1.0f;
        quizMusic.Play();
        StartCoroutine(MusicFade(quizMusic, fadeDuration, 0, 0.25f));
        StartCoroutine(MusicFade(backgroundMusic, fadeDuration, 0.5f, 0.0f));
    }
    
    public void StopQuiz()
    {
        float fadeDuration = 1.0f;
        StartCoroutine(MusicFade(quizMusic, fadeDuration, 0.25f, 0.0f));
        StartCoroutine(MusicFade(backgroundMusic, fadeDuration, 0.0f, 0.5f));
    }
    
    IEnumerator MusicFade(AudioSource audioSource, float duration, float startVolume, float targetVolume)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }
    }
}