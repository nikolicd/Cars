using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioMixer audioMixer;

    Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
    AudioMixerSnapshot muteSnapshot;
    AudioMixerSnapshot unMuteSnapshot;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        muteSnapshot = audioMixer.FindSnapshot("Mute");
        unMuteSnapshot = audioMixer.FindSnapshot("UnMute");
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            audioSources.Add(child.name, child.GetComponent<AudioSource>());
        }
        foreach (var button in FindObjectsOfType<Button>())
        {

        }
    }

    public bool IsPlaying(string source)
    {
        AudioSource audio = audioSources[source];
        return audio.isPlaying;
    }

    public void Play(string source)
    {
        AudioSource audio = audioSources[source];
        audio.Play();
    }

    public void Play(string source, bool loop)
    {
        AudioSource audio = audioSources[source];
        audio.loop = loop;
        audio.Play();
    }

    public void Stop(string source)
    {
        AudioSource audio = audioSources[source];
        audio.Stop();
    }

    public void MusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, value));
    }

    public float GetMusicVolume()
    {
        float value = 0;
        audioMixer.GetFloat("MusicVolume", out value);
        return ((value - (-80)) / (0 - (-80)));
    }

    public float GetSoundVolume()
    {
        float value = 0;
        audioMixer.GetFloat("SoundVolume", out value);
        return ((value - (-80)) / (0 - (-80))); 
    }

    public void SoundVolume(float value)
    {
        audioMixer.SetFloat("SoundVolume", Mathf.Lerp(-80, 0, value));
    }

    public void Mute(bool isMuted)
    {
        if (isMuted)
        {
            muteSnapshot.TransitionTo(0);
        }
        else
        {
            unMuteSnapshot.TransitionTo(0);
        }
    }
}
