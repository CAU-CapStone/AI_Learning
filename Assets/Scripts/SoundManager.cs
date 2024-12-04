using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private Dictionary<string, AudioClip> audioClipCache;
    private AudioSource audioSource;

    public AudioSource quizMusic;
    public AudioSource backgroundMusic;
    public AudioSource knnBackgroundMusic;
    public AudioSource dtBackgroundMusic;
    
    public AudioSource _footsteps;
    private AudioClip _insideFootsteps;
    private AudioClip _outsideFootsteps;

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
        //발소리 관련 
        _insideFootsteps = SoundManager.Instance.GetAudioClip("footsteps_wood");
        _outsideFootsteps = SoundManager.Instance.GetAudioClip("footsteps_grass");
        _footsteps = GameManager.Instance.player.GetComponent<AudioSource>();
        _footsteps.clip = _insideFootsteps;
    }
    
    //발걸음 소리 변경
    public void ChangeFootstep(bool inside)
    {
        _footsteps.clip = inside ? _insideFootsteps : _outsideFootsteps;
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

    public void PlaySoundOneShot(string soundName, float volume = 0.3f)
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
        StartCoroutine(MusicFade(quizMusic, fadeDuration, 0, 0.05f));
        StartCoroutine(MusicFade(backgroundMusic, fadeDuration, 0.1f, 0.0f));
    }
    
    public void StopQuiz()
    {
        float fadeDuration = 1.0f;
        StartCoroutine(MusicFade(quizMusic, fadeDuration, 0.05f, 0.0f));
        StartCoroutine(MusicFade(backgroundMusic, fadeDuration, 0.0f, 0.1f));
    }

    public void ToKnnMap()
    {
        float fadeDuration = 1.0f;
        StartCoroutine(ChangeBgm(knnBackgroundMusic, fadeDuration, 0.1f, 0.0f));
    }
    
    public void ToDTMap()
    {
        float fadeDuration = 1.0f;
        StartCoroutine(ChangeBgm(dtBackgroundMusic, fadeDuration, 0.1f, 0.0f));
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
    
    IEnumerator ChangeBgm(AudioSource audioSource, float duration, float startVolume, float targetVolume)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            backgroundMusic.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }

        backgroundMusic = audioSource;
        currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            backgroundMusic.volume = Mathf.Lerp(targetVolume, startVolume, currentTime / duration);
            yield return null;
        }
    }
}