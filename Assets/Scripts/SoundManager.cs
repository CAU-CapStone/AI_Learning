using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private Dictionary<string, AudioClip> audioClipCache;
    private AudioSource audioSource;

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
}